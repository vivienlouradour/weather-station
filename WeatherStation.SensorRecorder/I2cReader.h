#ifndef I2CREADER_H
#define I2CREADER_H

#include <iostream>
#include <string>
#include <stdlib.h>
#include <linux/i2c-dev.h>
#include <sys/ioctl.h>
#include <fcntl.h>
#include <unistd.h>

class I2cReader{
    private:
        int file_i2c;
        std::string bus;
    public:
        I2cReader();
        float humidity();
        float temperature();
};

#endif