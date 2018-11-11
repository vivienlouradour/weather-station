#ifndef API_H_
#define API_H_

#include <iostream>
#include <string>
#include <sstream> /* stringstream */
#include <unistd.h> /* read, write, close */
#include <sys/socket.h> /* socket, connect */
#include <netinet/in.h> /* struct sockaddr_in, struct sockaddr */
#include <netdb.h> /* struct hostent, gethostbyname */
#include <string.h> /* memcpy, memset */
#include "utils.h"

class Api{
    private:
        std::string host;
        int port;
        std::string broadcasterName;
        void postRequest(std::string body);

    public:
        Api(std::string host, int port, std::string broadcasterName);
        void sendRecord(float temperature, float humidity, char* date);
        void print();
};

#endif