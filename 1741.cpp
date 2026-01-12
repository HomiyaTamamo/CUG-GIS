#include <iostream>
#include <vector>
#include <cstring>

using namespace std;

const int MAXN = 10001;

vector<pair<int, int>> tree[MAXN]; // 存储树的结构
int dist[MAXN]; // 存储节点到根节点的距禿

// 计算节点到根节点的距离
void dfs(int node, int parent, int d) {
    dist[node] = d;
    for (int i = 0; i < tree[node].size(); i++) {
        int child = tree[node][i].first;
        if (child != parent) {
            dfs(child, node, d + tree[node][i].second);
        }
    }
}

// 计算有效节点对数量
int countValidPairs(int n, int k) {
    int validPairs = 0;

    for (int i = 1; i <= n; i++) {
        for (int j = 0; j < tree[i].size(); j++) {
            int child = tree[i][j].first;
            if (child > i) {
                int pathLength = dist[i] + dist[child] - 2 * dist[i];
                if (pathLength <= k) {
                    validPairs += (n - 1 - j);
                }
            }
        }
    }

    return validPairs;
}

int main() {
    int n, k;
    while (cin >> n >> k && n != 0 && k != 0) {
        // 初始化
        for (int i = 1; i <= n; i++) {
            tree[i].clear();
        }
        memset(dist, 0, sizeof(dist));

        // 构建树的结构
        for (int i = 1; i < n; i++) {
            int u, v, l;
            cin >> u >> v >> l;
            tree[u].push_back(make_pair(v, l));
            tree[v].push_back(make_pair(u, l));
        }

        // 计算节点到根节点的距禿
        dfs(1, -1, 0);

        // 计算有效节点对数量
        int validPairs = countValidPairs(n, k) / 2; // 每对节点对被计算了两次，除以2得到实际数量
        cout << validPairs << endl;
    }

    return 0;
}