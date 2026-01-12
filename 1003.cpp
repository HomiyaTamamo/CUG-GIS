#include <iostream>
using namespace std;

int main() {
    double c; // 用于存储输入的浮点数 
    while (cin >> c) { // 循环读取输入
        if (c == 0.00) { // 如果输入为 0.00，则结束循环
            break;
        }

        int cards = 1; // 初始化卡片数量为 1
        double overhang = 0.5; // 初始化悬挂长度为 0.5,个人测试阶段试着用过float，但float无法处理大于15的c，所以改成double了
        while (overhang < c) { // 循环直到悬挂长度达到或超过目标长度 c
            cards++; // 增加卡片数量
            overhang += 1.0 / (cards + 1); // 计算新的悬挂长度
        }

        cout << cards << " card(s)" << std::endl; // 输出所需的卡片数量
    }

    return 0; 
}