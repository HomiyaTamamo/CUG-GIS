#include <iostream>
#include <string>
#include <cctype>
using namespace std;

char letterToNumber(char letter) {
    if (letter >= 'A' && letter <= 'Z') {
        if (letter <= 'Q') return '2' + (letter - 'A') / 3;
        else if (letter <= 'Z') return '2' + (letter - 'A' - 1) / 3;
    }
    return letter;
}

std::string changePhoneNumber(const std::string& phoneNumber) {
    string change;
    for (size_t i = 0; i < phoneNumber.length(); i++) {
        char c = phoneNumber[i];
        if (isalnum(c)) {
            change += letterToNumber(toupper(c));
        }
    }
    return change.insert(3, "-");
}

int main() {
    int n;
    cin >> n;

    string phoneBook[10000];
    int count[10000] = { 0 };

    int index = 0;

    for (int i = 0; i < n; i++) {
        string phoneNumber;
        cin >> phoneNumber;

        string Number = changePhoneNumber(phoneNumber);

        int j;
        for (j = 0; j < index; j++) {
            if (phoneBook[j] == Number) {
                count[j]++;
                break;
            }
        }

        if (j == index) {
            phoneBook[index] = Number;
            count[index] = 1;
            index++;
        }
    }

    bool duplicatesFound = false;

    for (int i = 0; i < index; i++) {
        if (count[i] > 1) {
            cout << phoneBook[i] << " " << count[i] << endl;
            duplicatesFound = true;
        }
    }
    if (!duplicatesFound) {
        cout << "无重复号码" << endl;
    }
    return 0;
}
