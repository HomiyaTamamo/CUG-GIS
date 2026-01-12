#include <iostream>
#include <vector>
#include <algorithm>

using namespace std;

// 定义结构体用于存储DNA字符串及其排序度量值
struct DNAString {
    string sequence;
    int inversions;
};

// 比较函数，用于排序DNAString结构体
bool compareDNAStrings(const DNAString& a, const DNAString& b) {
    if (a.inversions == b.inversions) {
        return false; // 保持原始顺序
    }
    return a.inversions < b.inversions;
}

int countInversions(const string& str) {
    int inversions = 0;
    for (int i = 0; i < str.length(); i++) {
        for (int j = i + 1; j < str.length(); j++) {
            if (str[i] > str[j]) {
                inversions++;
            }
        }
    }
    return inversions;
}

int main() {
    int n, m;
    cin >> n >> m;

    vector<DNAString> dnaStrings(m);

    for (int i = 0; i < m; i++) {
        cin >> dnaStrings[i].sequence;
        dnaStrings[i].inversions = countInversions(dnaStrings[i].sequence);
    }

    sort(dnaStrings.begin(), dnaStrings.end(), compareDNAStrings);

    for (int i = 0; i < m; i++) {
        cout << dnaStrings[i].sequence << endl;
    }

    return 0;
}
