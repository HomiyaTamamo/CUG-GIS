#include <iostream>
#define NUM 101
using namespace std;
int init[NUM][NUM];
int color[NUM][NUM];
int roomnum = 0;
int roomarea = 0;
int max_roomarea = 0;

void DFS(int i, int j)
{
    if (color[i][j])//如果已经（访问过）被涂过;
        return;
    roomarea++;
    color[i][j] = roomnum;
    if ((init[i][j] & 1) == 0) DFS(i, j - 1);
    if ((init[i][j] & 2) == 0) DFS(i - 1, j);
    if ((init[i][j] & 4) == 0) DFS(i, j + 1);
    if ((init[i][j] & 8) == 0) DFS(i + 1, j);
}

int main()
{
    int R, C;
    cin >> R >> C;
    for (int i = 1; i <= R; i++)
        for (int j = 1; j <= C; j++)
            cin >> init[i][j];
    memset(color, 0, sizeof(color));
    for (int i = 1; i <= R; i++)
        for (int j = 1; j <= C; j++) {
            if (!color[i][j]) {//表示没有访问过的节点；（也就是没有进行涂过的地方）
                roomnum++;      //记录房间的个数；
                roomarea = 0;   //记录房间的区域数；
                DFS(i, j);//i和j 才将一个节点确定；
                max_roomarea = max(max_roomarea, roomarea);
            }
        }

    cout << roomnum << endl << max_roomarea << endl;
    return 0;
}

