#include <iostream>
#include <vector>

using namespace std;

int countWays(int m, int n) {
    // 如果苹果数为0或盘子数为1，则只有一种分法
    if (m == 0 || n == 1) {
        return 1;
    }

    // 如果苹果数小于盘子数，等同于将m个苹果放入m个盘子中
    if (m < n) {
        return countWays(m, m);
    }

    // 递归计算将m个苹果放入n-1个盘子中的分法数量，以及将m-n个苹果放入n个盘子中的分法数量
    return countWays(m, n - 1) + countWays(m - n, n);
}

int main() {
    int t;
    cin >> t; // 读取测试数据的数量

    while (t--) {
        int m, n;
        cin >> m >> n; // 读取每组测试数据中的苹果数和盘子数

        int ways = countWays(m, n); // 计算不同分法数量
        cout << ways << endl; // 输出结果
    }

    return 0;
}