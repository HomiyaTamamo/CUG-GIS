#include <iostream>
#include <vector>
#include <algorithm>

using namespace std;

struct Run {
    int teamNumber;
    int problemNumber;
    int submissionTime;
    int result;
};

struct Team {
    int teamNumber;
    int solvedProblems = 0;
    int totalTime = 0;
};

bool compareTeams(const Team& a, const Team& b) {
    if (a.solvedProblems != b.solvedProblems) {
        return a.solvedProblems > b.solvedProblems;
    }
    else {
        return a.totalTime < b.totalTime;
    }
}

int main() {
    int C, N;
    cin >> C >> N;

    vector<Run> runs(N);
    for (int i = 0; i < N; ++i) {
        cin >> runs[i].teamNumber >> runs[i].problemNumber >> runs[i].submissionTime >> runs[i].result;
    }

    vector<Team> teams(C);
    for (const auto& run : runs) {
        Team& team = teams[run.teamNumber - 1];
        if (run.result == 1) {
            if (team.solvedProblems == 0) {
                team.totalTime += run.submissionTime / 60 + 20 * (team.solvedProblems);
            }
            else {
                team.totalTime += (run.submissionTime / 60 - team.totalTime) + 20;
            }
            team.solvedProblems++;
        }
    }

    sort(teams.begin(), teams.end(), compareTeams);

    for (const auto& team : teams) {
        cout << team.teamNumber << " ";
    }

    return 0;
}
