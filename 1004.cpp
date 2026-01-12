#include <iostream>
using namespace std;
int main()
{
	double Jan;
	double Feb;
	double Mar;
	double Apr;
	double May;
	double Jun;
	double Jul;
	double Aug;
	double Sep;
	double Oct;
	double Nov;
	double Dec;//定义余额为浮点数
	cin >> Jan;
	cin >> Feb;
	cin >> Mar;
	cin >> Apr;
	cin >> May;
	cin >> Jun;
	cin >> Jul;
	cin >> Aug;
	cin >> Sep;
	cin >> Oct;
	cin >> Nov;
	cin >> Dec;//输入余额
	double avg = (Jan + Feb + Mar + Apr + May + Jun + Jul + Aug + Sep + Oct + Nov + Dec) / 12;//计算平均余额
	cout <<"$"<< avg << endl;//输出结果
}


//#include <iostream>
//#include <iomanip> // 用于设置输出精度
//using namespace std;
//
//int main()
//{
//    const int numMonths = 12;
//    double balance[numMonths]; // 用于存储每个月的余额
//
//    for (int i = 0; i < numMonths; i++) {
//        cin >> balance[i]; // 输入每个月的余额
//    }
//
//    double totalBalance = 0.0;
//    for (int i = 0; i < numMonths; i++) {
//        totalBalance += balance[i]; // 计算总余额
//    }
//
//    double avg = totalBalance / numMonths; // 计算平均余额
//
//    cout << fixed << setprecision(2); // 设置输出精度为两位小数
//    cout << "$" << avg << endl; // 输出平均余额
//
//    return 0;
//}

