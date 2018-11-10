#include <iostream>
#include "I2cReader.h"

using namespace std;
int main(){
    cout << "hello world! "<< endl;
    
    I2cReader i2cReader;
    try{
        float hum = i2cReader.humidity();
        float temp = i2cReader.temperature();
        cout << "Humidity : " << hum << endl;
        cout << "Temperature : " << temp << endl;
    }
    catch(const exception&){
        return EXIT_FAILURE;
    }
}