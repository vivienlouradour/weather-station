#include <stdio.h> /* printf, sprintf */
#include <stdlib.h> /* exit */
#include <unistd.h> /* read, write, close */
#include <string.h> /* memcpy, memset */
#include <sys/socket.h> /* socket, connect */
#include <netinet/in.h> /* struct sockaddr_in, struct sockaddr */
#include <netdb.h> /* struct hostent, gethostbyname */
#include <linux/i2c-dev.h>
#include <sys/ioctl.h>
#include <fcntl.h>
#include <time.h>
#include <string.h>

int recordDelay = 600;
void error(const char *msg) { perror(msg); exit(0); }


int postData(char* date, float temp, float hum, char* broadcasterName)
{
    /* first what are we going to send and where are we going to send it? */
    int portno = 5000;
    char *host = "ninsdev.tk";
    
    char body[256];
    sprintf(body, "{\"DateTime\":\"%s\",\"Temperature\": %f,\"Humidity\": %f,\"BroadcasterName\":\"%s\"}", date, temp, hum, broadcasterName);
    char *header = "POST /weatherstation/api/record/ HTTP/1.1\r\nHost: ninsdev.tk:5000\r\nContent-Type: application/json\r\nCache-Control: no-cache\r\nContent-Length: ";
    
    char contentLength[256];
    snprintf(contentLength, sizeof contentLength, "%zu", strlen(body));
    char *message = malloc(strlen(header) + strlen(contentLength) + strlen("\r\n\r\n") + strlen(body) + 1);
    strcpy(message, header);
    strcat(message, contentLength);
    strcat(message, "\r\n\r\n");
    strcat(message, body);



    struct hostent *server;
    struct sockaddr_in serv_addr;
    int sockfd, bytes, sent, received, total;
    char response[4096];    

    /* fill in the parameters */
    printf("Request:\n%s\n",message);
    /* create the socket */
    sockfd = socket(AF_INET, SOCK_STREAM, 0);

    if (sockfd < 0) error("ERROR opening socket");

    /* lookup the ip address */
    server = gethostbyname(host);

    if (server == NULL) error("ERROR, no such host");

    /* fill in the structure */
    memset(&serv_addr,0,sizeof(serv_addr));
    serv_addr.sin_family = AF_INET;
    serv_addr.sin_port = htons(portno);
    memcpy(&serv_addr.sin_addr.s_addr,server->h_addr_list[0],server->h_length);

    /* connect the socket */
    if (connect(sockfd,(struct sockaddr *)&serv_addr,sizeof(serv_addr)) < 0)
        error("ERROR connecting");

    /* send the request */
    total = strlen(message);
    sent = 0;
    do {
        bytes = write(sockfd,message+sent,total-sent);
        if (bytes < 0)
            error("ERROR writing message to socket");
        if (bytes == 0)
            break;
        sent+=bytes;
    } while (sent < total);

    memset(response,0,sizeof(response));
    total = sizeof(response)-1;
    received = 0;
    bytes = read(sockfd,response+received,total-received);
    
    if(strpbrk(response, "201 Created") == NULL){
        printf("API Server error. \n Response : \n%s", response);
        return 1;
    }
    printf("Sent and created\n");


    /* close the socket */
    close(sockfd);
    return 0;
 
}

int main(int argc, char *argv[]){
    if(argc < 2){
        printf("Broadcaster name parameter missing");
        return 1;
    }
    char* broadcasterName = argv[1];
    printf("Broadcaster name : %s", broadcasterName);
    // Create I2C bus
	int file;
	char *bus = "/dev/i2c-1";
	if((file = open(bus, O_RDWR)) < 0) 
	{
		printf("Failed to open the bus. \n");
		exit(1);
	}
	// Get I2C device, SI7021 I2C address is 0x40(64)
	ioctl(file, I2C_SLAVE, 0x40);

    while(1){
        printf("New record...\n");
        // Send humidity measurement command(0xF5)
		char config[1] = {0xF5};
		write(file, config, 1);
		sleep(1);

		// Read 2 bytes of humidity data
		// humidity msb, humidity lsb
		char data[2] = {0};
        float humidity;
		if(read(file, data, 2) != 2)
		{
			printf("Error : Input/output Error \n");
            return 1;
		}
		else
		{
			// Convert the data
			humidity = (((data[0] * 256 + data[1]) * 125.0) / 65536.0) - 6;

			// Output data to screen
			printf("Relative Humidity : %.2f RH \n", humidity);
		}

		// Send temperature measurement command(0xF3)
		config[0] = 0xF3;
		write(file, config, 1); 
		sleep(1);

		// Read 2 bytes of temperature data
		// temp msb, temp lsb
        float cTemp;
		if(read(file, data, 2) != 2)
		{
			printf("Error : Input/output Error \n");
            return 1;
		}
		else
		{
			// Convert the data
			cTemp = (((data[0] * 256 + data[1]) * 175.72) / 65536.0) - 46.85;
			float fTemp = cTemp * 1.8 + 32;

			// Output data to screen
			printf("Temperature in Celsius : %.2f C \n", cTemp);
			printf("Temperature in Fahrenheit : %.2f F \n", fTemp);
		}

        time_t t = time(NULL);
        struct tm tm = *localtime(&t);
        char date[256];
        sprintf(date, "%d-%02d-%02dT%02d:%02d:%02d.000Z", tm.tm_year + 1900, tm.tm_mon + 1, tm.tm_mday, tm.tm_hour, tm.tm_min, tm.tm_sec);
        if(postData(date, cTemp, humidity, broadcasterName) != 0){
            printf("erreur sending post request");
            return 1;
        }

        printf("sleeping for %d seconds\n", recordDelay);
        sleep(600);
    }
    return 0;
}