#include <iostream>
#include <string>
#include <vector>
using namespace std;

int main() {
    vector<string> read = { "a","b","c","d","e","f","g","h","i","j","k","l","m","n","o","p","q","r","s","t","u","v","w","x","y","z"," " };
    cout << "输入加密/解密密钥" << endl;
    int input1, input2;
    cin >> input1 >> input2;
    bool stop = true;
    cout << "请选择加密(1)/解密(2)" << endl;
    int start;
    cin >> start;
    switch (start) {
    case 1: {
        while (stop) {
            string message;
            getline(cin, message);
            for (int i = 0; i < read.size(); i++) {
                if (message == read[i]) {
                    cout << (i ^ input1) % input2 << endl;
                    i = read.size();
                }
                else if (message == "stop") {
                    stop = false;
                }
            }
        }
        break;
    }
    case 2: {
        while (stop) {
            int message;
            cin >> message;
            if (message < 0) {
                stop = false;
            }
            else {
                cout << read[(message ^ input1) % input2] << endl;
            }
        }
        break;
    }
    default:
        stop = false;
        break;
    }
    return 0;
}
