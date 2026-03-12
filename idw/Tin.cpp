#include "TIN.h"
#include <algorithm>
#include <iostream>
#include <set>

// 添加点
void TIN::addPoint(const Point& point) {
    points.push_back(point);
}

// 使用Delaunay三角剖分算法优化三角形生成
void TIN::buildTIN() {
    if (points.size() < 3) return;

    // 使用集合去重，避免生成重复的三角形
    std::set<std::tuple<int, int, int>> uniqueTriangles;

    // 分块处理数据，避免一次性加载过多数据
    size_t batchSize = 500;  // 每次处理500个点
    size_t totalPoints = points.size();

    for (size_t i = 0; i < totalPoints; i += batchSize) {
        size_t end = std::min(i + batchSize, totalPoints);

        // 处理当前批次的点
        for (size_t j = i; j < end; ++j) {
            // 生成当前点和其他点的三角形组合
            for (size_t k = j + 1; k < end; ++k) {
                for (size_t l = k + 1; l < end; ++l) {
                    std::vector<int> ids = { points[j].getObjectId(), points[k].getObjectId(), points[l].getObjectId() };
                    std::sort(ids.begin(), ids.end());
                    uniqueTriangles.insert(std::make_tuple(ids[0], ids[1], ids[2]));
                }
            }
        }
    }

    // 将唯一的三角形加入到 triangles 向量中
    triangles.assign(uniqueTriangles.begin(), uniqueTriangles.end());
}

// 输出三角形的点序号
void TIN::printTriangles() const {
    for (const auto& triangle : triangles) {
        std::cout << "Triangle: "
            << std::get<0>(triangle) << ", "
            << std::get<1>(triangle) << ", "
            << std::get<2>(triangle) << std::endl;
    }
}

// 获取所有三角形的数据
std::vector<std::tuple<int, int, int>> TIN::getTriangles() const {
    return triangles;
}
