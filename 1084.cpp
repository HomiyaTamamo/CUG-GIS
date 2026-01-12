#include <iostream>
#include <vector>
#include <algorithm>
#include <cstring>

using namespace std;

int T, n, k, totstick, totsquare, neibor, ans, maxd, exi[65], temp[65];

vector<int> stick[65];     // 需要第 i 根火柴棍的正方形
vector<int> square[65];    // 第 i 个正方形需要的火柴棍

// 估算剩余步骤数的启发式函数
int h()
{
    int res = 0;
    for (int i = 1; i <= totsquare; i++)
        temp[i] = exi[i];

    for (int i = 1; i <= totsquare; i++)
    {
        if (!temp[i])
        {
            res++;
            temp[i] -= square[i].size();
            for (int j = 0; j < square[i].size(); j++)
            {
                for (int l = 0; l < stick[square[i][j]].size(); l++)
                {
                    temp[stick[square[i][j]][l]]--;
                }
            }
        }
    }
    return res;
}

// 深度优先搜索找到最优解
bool dfs(int sum)
{
    if (sum + h() >= maxd)
        return false;

    int tmp = 1;
    while (exi[tmp] < 0 && tmp <= totsquare)
        tmp++;  // 选择一个正方形（tmp）
    if (tmp > totsquare)
    {
        ans = min(sum, ans);
        return true;
    }

    for (int i = 0; i < square[tmp].size(); i++)
    {
        int sti = square[tmp][i];
        for (int j = 0; j < stick[sti].size(); j++)
        {
            exi[stick[sti][j]]--;
        }

        if (dfs(sum + 1))
            return true;

        for (int j = 0; j < stick[sti].size(); j++)
        {
            exi[stick[sti][j]]++;
        }
    }
    return false;
}

int main()
{
    cin >> T;
    while (T--)
    {
        cin >> n >> k;
        totstick = (n + 1) * n * 2;
        totsquare = 0;
        neibor = n * 2 + 1;

        for (int i = 1; i < 65; i++)
        {
            stick[i].clear();
            square[i].clear();
        }

        // 生成正方形和火柴棍
        for (int sz = 1; sz <= n; sz++)
        {
            for (int i = 1; (i - 1) / neibor + sz <= n; i += neibor)
            {
                for (int j = i; j - i + sz <= n; j++)
                {
                    totsquare++;
                    for (int l = j; l - j < sz; l++)
                    {
                        square[totsquare].push_back(l);
                        square[totsquare].push_back(l + sz * neibor);
                        stick[l].push_back(totsquare);
                        stick[l + sz * neibor].push_back(totsquare);
                    }
                    for (int l = j + n; (l - j - sz) / neibor < sz; l += neibor)
                    {
                        square[totsquare].push_back(l);
                        square[totsquare].push_back(l + sz);
                        stick[l].push_back(totsquare);
                        stick[l + sz].push_back(totsquare);
                    }
                }
            }
        }

        memset(exi, 0, sizeof exi);
        for (int i = 1; i <= k; i++)
        {
            int t;
            cin >> t;
            for (int j = 0; j < stick[t].size(); j++)
            {
                exi[stick[t][j]]--;
            }
            totstick--;
        }

        ans = totstick;
        for (maxd = 1;; maxd++)
        {
            if (dfs(0))
                break;
        }
        cout << ans << endl;
    }
    return 0;
}
