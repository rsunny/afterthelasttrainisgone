#include <bits/stdc++.h>

using namespace std;

string num;

void solve(){
	cin>>num;
	int n=num.size();
	int i=0;
	int j=n-1;
	while(i+1<j){
		i++;
		j--;
	}
	cout<<i<<" "<<j;
	return;
}

int main(){
	int TEST;
	cin>>TEST;
	for(int test=1;test<=TEST;test++){
		printf("Case %d: ",test);
		solve();
		printf("\n");
	}
	return 0;
}