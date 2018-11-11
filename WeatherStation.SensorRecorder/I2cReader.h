#ifndef I2CREADER_H_
#define I2CREADER_H_

#include <iostream>
#include <string>
#include <stdlib.h>
#include <linux/i2c-dev.h>
#include <sys/ioctl.h>
#include <fcntl.h>
#include <unistd.h>
#include "utils.h"

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