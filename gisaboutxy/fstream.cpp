#include <fstream>
#include <vector>
#include <string>
#include <iostream>

struct Coordinate {
    double x;
    double y;
};

std::vector<Coordinate> readCoordinatesFromFile(const std::string& filePath) {
    std::vector<Coordinate> coordinates;
    std::ifstream file(filePath);
    if (!file.is_open()) {
        std::cerr << "Failed to open file: " << filePath << std::endl;
        return coordinates;
    }

    double x, y;
    while (file >> x >> y) {
        coordinates.push_back({ x, y });
    }

    file.close();
    return coordinates;
}
