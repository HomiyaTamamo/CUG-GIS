#include <iostream>
#include <vector>
#include <algorithm>

using namespace std;

long long mergeAndCount(vector<int>& arr, int low, int mid, int high) {
    vector<int> temp(high - low + 1);
    long long inversions = 0;

    int i = low, j = mid + 1, k = 0;

    while (i <= mid && j <= high) {
        if (arr[i] <= arr[j]) {
            temp[k++] = arr[i++];
        }
        else {
            temp[k++] = arr[j++];
            inversions += mid - i + 1;
        }
    }

    while (i <= mid) {
        temp[k++] = arr[i++];
    }

    while (j <= high) {
        temp[k++] = arr[j++];
    }

    for (int i = low, k = 0; i <= high; ++i, ++k) {
        arr[i] = temp[k];
    }

    return inversions;
}

long long mergeSortAndCount(vector<int>& arr, int low, int high) {
    long long inversions = 0;

    if (low < high) {
        int mid = low + (high - low) / 2;
        inversions += mergeSortAndCount(arr, low, mid);
        inversions += mergeSortAndCount(arr, mid + 1, high);
        inversions += mergeAndCount(arr, low, mid, high);
    }

    return inversions;
}

int main() {
    int n;
    while (cin >> n && n != 0) {
        vector<int> arr(n);
        for (int i = 0; i < n; ++i) {
            cin >> arr[i];
        }

        long long swaps = mergeSortAndCount(arr, 0, n - 1);
        cout << swaps << endl;
    }

    return 0;
}
