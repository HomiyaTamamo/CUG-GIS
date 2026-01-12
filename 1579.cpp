#include <iostream>
#include <iomanip>
#include <algorithm>
#define MAXN 21
using namespace std;
int dp[MAXN][MAXN][MAXN];
int w(int a, int b, int c)
{
    if (a <= 0 || b <= 0 || c <= 0) {
        return 1;//为了防止下标越界(为负)所以先处理
    }
    if (a > 20 || b > 20 || c > 20) {
        return dp[20][20][20] = w(20, 20, 20);//为了防止下标越界(大于20)所以先处理
    }
    if (dp[a][b][c] != 0) {
        return dp[a][b][c];//已经计算过的不再重复计算
    }
    if (a < b && b < c)  {
        return dp[a][b][c] = (w(a, b, c - 1) + w(a, b - 1, c - 1) - w(a, b - 1, c));
        return dp[a][b][c] = (w(a - 1, b, c) + w(a - 1, b - 1, c) + w(a - 1, b, c - 1) - w(a - 1, b - 1, c - 1));
    }
}
int main()
{
    memset(dp, 0, sizeof(dp));
    int a, b, c;
    while (cin >> a >> b >> c)
    {
        if (a == -1 && b == -1 && c == -1) {
            break;
        }
        cout << "w(" << a << ", " << b << ", " << c << ") = " << w(a, b, c) << endl;
    }
    return 0;
}
