#include <iostream>
#include <cmath>
#define M_PI       3.14159265358979323846

int main() {
    int N;
    std::cin >> N; // 读取数据集的数量

    for (int i = 1; i <= N; i++) {
        double x, y;
        std::cin >> x >> y; // 读取每个数据集的坐标

        // 计算数据集点到圆心的距离
        double distance = sqrt(x * x + y * y);

        // 计算数据集点在圆内的面积
        double area = M_PI * distance * distance / 2.0;

        int year = 1;
        while (area > 0.0) {
            area -= 50.0; // 每年减少 50 平方英里
            year++;
        }
        std::cout << "Property " << i << ": This property will begin eroding in year " << year-1 << "." << std::endl;
        
    }
    
    std::cout << "END OF OUTPUT." << std::endl;

    return 0;
}

