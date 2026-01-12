#include <iostream>
#include <algorithm>
using namespace std;

typedef pair<int, int> Wood;

Wood a[5005];

int q[5005];
int qSize;

int main()
{
	int T, n;//案例数 对象数

	cin >> T;

	for (; T; T--)
	{
		cin >> n;

		qSize = 1;
		q[0] = 0;

		for (int i = 0; i < n; i++)
		{
			cin >> a[i].first >> a[i].second;
		}

		sort(a, a + n);

		int lb, rb, mid;

		for (int i = 0; i < n; i++)
		{
			lb = -1, rb = qSize;

			while (rb - lb > 1)
			{
				mid = (lb + rb) >> 1;

				if (q[mid] > a[i].second)
				{
					lb = mid;
				}
				else
				{
					rb = mid;
				}
			}

			q[rb] = a[i].second;
			if (rb == qSize)
			{
				qSize++;
			}
		}

		cout << qSize << '\n';

	}

	return 0;
}