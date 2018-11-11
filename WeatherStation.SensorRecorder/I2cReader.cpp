#include "I2cReader.h"


using namespace std;

I2cReader::I2cReader(){
    logInfo("I2C reader initialization...");
    this->bus = "/dev/i2c-1";

    //Create I2C bus
    if((this->file_i2c = open(bus.c_str(), O_RDWR)) < 0){
        logError("Failed to open the I2C bus.");
        throw exception();
    }

	// Get I2C device, SI7021 I2C address is 0x40(64)
    if(ioctl(this->file_i2c, I2C_SLAVE, 0x40) < 0){
        logError("Failed to acquire bus access and/or talk to slave.");
        throw exception();
    }

    logInfo("I2C reader initialized");
}

float I2cReader::humidity(){
    logInfo("Reading new humidity record...");

    // Send humidity measurement command(0xF5)
    unsigned char config[1] = {0xF5};
    if(write(this->file_i2c, config, 1) != 1){
        logError("Writing humidity command :failed to write to the i2c bus.");
        throw exception();
    } 
    sleep(1);

    // Read 2 bytes of humidity data
    // humidity msb, humidity lsb
    unsigned char data[2] = {0};
    float humidity;
    if(read(this->file_i2c, data, 2) != 2)
    {
        logError("Input/output Error : impossible to read humidity record.");
        throw exception();
    }
    
    // Convert the data
    humidity = (((data[0] * 256 + data[1]) * 125.0) / 65536.0) - 6;

    return humidity;
}

float I2cReader::temperature(){
    logInfo("Reading new temperature record...");

    // Send temperature measurement command(0xF3)
    unsigned char config[1] = {0xF3};
    if(write(this->file_i2c, config, 1) != 1){
        logError("Writing temperature command :failed to write to the i2c bus.");
        throw exception();
    } 
    sleep(1);

    // Read 2 bytes of temperature data
    // temp msb, temp lsb
    float cTemp;
    char data[2] = {0};
    if(read(this->file_i2c, data, 2) != 2)
    {
        logError("Error : Input/output Error : impossible to read temperature record.");
        throw exception();
    }

    // Convert the data to celsus
    cTemp = (((data[0] * 256 + data[1]) * 175.72) / 65536.0) - 46.85;   

    return cTemp;
}

