#include <iostream>
#include <vector>
#include <algorithm>
using namespace std;
int main() {
    int T;
    cin >> T;
    if (T <= 20 && T >= 1) {
        for (int t = 0; t < T; ++t) {
            int N;
            cin >> N;
            vector<int> times(N);
            for (int i = 0; i < N; ++i) {
                cin >> times[i];
            }

            sort(times.begin(), times.end());

            int totalTime = 0;
            while (N > 3) {
                int option1 = times[0] + times[1] + times[N - 1] + times[1]; // 最快和次快过去，最快回；最慢和次慢过去，次快回
                int option2 = times[0] + times[N-1] + times[0] + times[N - 2]; // 最快和最慢过，最快回；最快和次慢过去，最快回
                totalTime += min(option1, option2);
                N -= 2;
            }

            if (N == 1) {
                totalTime += times[0];
            }
            else if (N == 2) {
                totalTime += times[1];
            }
            else if (N == 3) {
                totalTime += times[0] + times[1] + times[2];
            }

            cout << totalTime << endl;
        }

        return 0;
    }
    else {
        return 0;
    }

    
}


