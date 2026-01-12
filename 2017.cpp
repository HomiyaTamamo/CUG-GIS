#include <iostream>
using namespace std;
int main()
{
    int times;//定义记录次数
    while (true) {
        cin >> times;
        if (times <= 0) {
            break;
        }
        int distance = 0;
        int prevtime = 0;
        for (int i = 0; i < times; i++) {
            int speed, time;
            cin >> speed >> time;
            distance += speed * (time - prevtime);//累加计算距离
            prevtime = time;//更新时间
        }
        cout << distance << " miles" << endl;
    }
}


