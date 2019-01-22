#ifndef UTILS_H_
#define UTILS_H_

#include <string>
#include <iostream>
#include <stdio.h>
#include <fstream>

void logError(std::string message);
void logInfo(std::string message);
std::string getCurrentDate();
void writeConsole(std::string message);
void writeFile(std::string message);

#endif