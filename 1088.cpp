#include <iostream>
#include <algorithm>
using namespace std;
int a[105][105];
bool vis[105][105];         //标记当前点是否访问过
int path[4][2] = { 1, 0, -1, 0, 0, -1, 0, 1 };    //4个方向：下，上，左，右
int mem[105][105];         //记忆化数组，表示从第x行y列开始的最长下降序列的长度
int r, c;
bool checkedge(int xx, int yy)  //判断此点是否遍历过，或者该点是否在所给范围内
{
    if (xx >= 1 && xx <= r && yy >= 1 && yy <= c && !vis[xx][yy])
    {
        return 1;
    }
    else
        return 0;
}
int Max = 0;
int dfs(int x, int y)   //从深度优先方面考虑，遍历到无路可走或者到一个已经搜过的点时才开始计数
{

    if (mem[x][y])       //如果当前点已经搜过，直接返回其值
    {
        return mem[x][y];
    }
    int tmp = 0;
    for (int i = 0; i < 4; i++)    //四个方向
    {
        int xx = path[i][0] + x;   //下一个位置
        int yy = path[i][1] + y;
        if (checkedge(xx, yy) && a[xx][yy] < a[x][y])   //下一个位置的高度需小于当前位置的高度
        {
            vis[xx][yy] = 1;        //标记当前位置为已遍历
            tmp = max(tmp, dfs(xx, yy));   //遍历出所有分路里最大的一个
            vis[xx][yy] = 0;        //取消标记，以便于从其他路径遍历该点
        }
    }
    mem[x][y] = tmp + 1;    //当前值为所有分路最大值+1(即加上当前位置)
    return tmp + 1;     //返回当前路径的值
}
int main()
{
    cin >> r >> c;
    for (int i = 1; i <= r; i++)
    {
        for (int j = 1; j <= c; j++)
        {
            cin >> a[i][j];
        }
    }
    for (int i = 1; i <= r; i++)
    {
        for (int j = 1; j <= c; j++)
        {
            memset(vis, 0, sizeof(vis));    //遍历时，需先初始化vis数组
            vis[i][j] = 1;
            Max = max(Max, dfs(i, j));     //遍历所有点，求出最大值
        }
    }
    cout << Max << endl;
    return 0;
}
