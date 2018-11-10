#include "Api.h"


using namespace std;

Api::Api(string host, int port, string broadcasterName){
    this->host = host;
    this->port = port;
    this->broadcasterName = broadcasterName;
}

void Api::print(){
    cout << "Host : " << this->host << " ; Port : " << this->port << " broadcasterName : " << this->broadcasterName << endl;
}