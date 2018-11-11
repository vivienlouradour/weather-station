#include "Api.h"


using namespace std;

Api::Api(string host, int port, string broadcasterName){
    this->host = host;
    this->port = port;
    this->broadcasterName = broadcasterName;

    ostringstream ss;
    ss << this->port;
    logInfo("Api initialized (Host=" + this->host + ", Port=" + ss.str() + ", BroadcasterName=" + this->broadcasterName);
}

void Api::sendRecord(float temperature, float humidity, string date){
    stringstream stream;
    stream << "{\"DateTime\":\"" << date << "\",\"Temperature\": "<< temperature << ",\"Humidity\": "<< humidity <<",\"BroadcasterName\":\"" << this->broadcasterName << "\"}";
    string jsonBody = stream.str();

    postRequest(jsonBody);
}

void Api::postRequest(string body){ 
    string apiRoute = "/weatherstation/api/record/";

    //POST request
    stringstream requestStream;
    requestStream << "POST " << apiRoute << " HTTP/1.1";
    requestStream << "\r\n";
    requestStream << "Host: " << this->host << ":" << this->port;
    requestStream << "\r\n";
    requestStream << "Content-Type: application/json";
    requestStream << "\r\n";
    requestStream << "Cache-Control: no-cache;";
    requestStream << "\r\n";
    requestStream << "Content-Length: " << body.length();
    requestStream << "\r\n\r\n";
    requestStream << body;
    
    string requestStr = requestStream.str();

    logInfo("Request : \n" + requestStr);

    struct hostent *server;
    struct sockaddr_in serv_addr;
    int sockfd, bytes, sent, received, total;
    char response[4096];    


    /* create the socket */
    sockfd = socket(AF_INET, SOCK_STREAM, 0);
    if (sockfd < 0){ 
        logError("opening socket");
        throw exception();
    }

    /* lookup the ip address */
    server = gethostbyname(this->host.c_str());
    if (server == NULL){
        logError("impossible to resolve host (" + this->host + ")");
        throw exception();
    } 

    /* fill in the structure */
    memset(&serv_addr,0,sizeof(serv_addr));
    serv_addr.sin_family = AF_INET;
    serv_addr.sin_port = htons(this->port);
    memcpy(&serv_addr.sin_addr.s_addr,server->h_addr_list[0],server->h_length);

    /* connect the socket */
    if (connect(sockfd,(struct sockaddr *)&serv_addr,sizeof(serv_addr)) < 0){
        logError("connecting socket");
        throw exception();
    }

    /* send the request */
    total = requestStr.length();
    sent = 0;
    do {
        bytes = write(sockfd,requestStr.c_str()+sent,total-sent);
        if (bytes < 0){
            logError("writing request to socket");
            throw exception();
        }
        if (bytes == 0)
            break;
        sent+=bytes;
    } while (sent < total);

    memset(response,0,sizeof(response));
    total = sizeof(response)-1;
    received = 0;
    bytes = read(sockfd,response+received,total-received);
    
    if(strpbrk(response, "201 Created") == NULL){
        logError("API Server error. \n Response : \n" + string(response));
        throw exception();
    }

    /* close the socket */
    close(sockfd);

    logInfo("Sent and created");
}