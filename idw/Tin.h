#ifndef TIN_H
#define TIN_H

#include "Point.h"
#include <vector>
#include <tuple>
#include <QString>

class TIN {
public:
    void addPoint(const Point& point);
    void buildTIN();
    void printTriangles() const;
    std::vector<std::tuple<int, int, int>> getTriangles() const;

private:
    std::vector<Point> points;           // ´æ´¢µã
    std::vector<std::tuple<int, int, int>> triangles;  // ´æ´¢Èý½ÇÐÎ
};

#endif // TIN_H
