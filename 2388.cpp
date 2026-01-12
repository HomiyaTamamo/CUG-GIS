#include <iostream>
#include <vector>
#include <algorithm>

using namespace std;

int main() {
    int N;
    cin >> N;

    vector<int> milkOutputs(N);

    for (int i = 0; i < N; i++) {
        cin >> milkOutputs[i];
    }

    sort(milkOutputs.begin(), milkOutputs.end());

    int median = milkOutputs[N / 2]; // 中位数即为排序后的数组的中间值

    cout << median << endl;

    return 0;
}