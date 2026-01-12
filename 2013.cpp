#include <iostream>
#include <string>
using namespace std;
int main() {
    int setNumber = 1; // 用于跟踪数据集的编号
    while (true) {
        int n;
        cin >> n; // 读取数据集中字符串的数量
        if (n == 0) {
            break; // 如果字符串数量为 0，则结束循环
        }
        string names[15]; 
        // 读取每个字符串并存储在数组中
        for (int i = 0; i < n; i++) {
            cin >> names[i];
        }
        string result[15]; // 创建存储重新排列后字符串的数组
        // 重新排列字符串
        int left = 0, right = n - 1;
        for (int i = 0; i < n; i++) {
            if (i % 2 == 0) {
                result[left] = names[i]; // 将左指针指向的字符串放在结果数组中
                left++;
            }
            else {
                result[right] = names[i]; // 将右指针指向的字符串放在结果数组中
                right--;
            }
        }
        cout << "SET " << setNumber << endl; // 输出数据集编号
        // 输出重新排列后的字符串
        for (int i = 0; i < n; i++) {
            cout << result[i] << endl;
        }
        setNumber++; // 更新数据集编号
    }
    return 0;
}

