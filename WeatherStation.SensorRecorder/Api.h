#ifndef API_H_
#define API_H_

#include <iostream>
#include <string>
#include "utils.h"

class Api{
    private:
        std::string host;
        int port;
        std::string broadcasterName;

    public:
        Api(std::string host, int port, std::string broadcasterName);
        void print();
};

#endif