#ifndef API_H_
#define API_H_

#include <iostream>
#include <string>
// #include <stdlib.h>
// #include <linux/i2c-dev.h>
// #include <sys/ioctl.h>
// #include <fcntl.h>
// #include <unistd.h>

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