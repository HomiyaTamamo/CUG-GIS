#include<iostream>
#include<algorithm>
using namespace std;
int n, m, x[1005], dp[1005][35];
int main() {
    cin >> n >> m;
    for (int i = 1; i <= n; i++) {
        cin >> x[i];
        x[i]--;
    }
    for (int i = m; i >= 0; i--) {
        for (int j = 1; j <= n; j++) {
            int p = i & 1;
            if (p == x[j]) dp[j][i] = max(max(dp[j - 1][i] + 1, dp[j][i]), dp[j - 1][i + 1]);
            else {
                dp[j][i] = max(dp[j - 1][i], dp[j][i]);
                if (i < m) dp[j][i] = max(dp[j][i], dp[j - 1][i + 1] + 1);
            }
        }
    }
        
    int ans = 0;
    for (int i = 0; i <= m; i++) {
        ans = max(ans, dp[n][i]);
    }
    cout<<ans<<endl;
    return 0;
}


