#include <bits/stdc++.h>

using namespace std;

void solve(int test){
	long long int num;
	scanf("%lld",&num);
	printf("Case %d: ",test);
	long long int d = ceil(sqrt(num));
	int f=(d%2LL);
	d--;
	long long int mid = (d*d)+1LL;
	d++;
	mid+=(d*d);
	mid/=2LL;
	long long int x=d,y=d;
	d=num-mid;
	if(d<0)
		y+=d;
	else if(d>0)
		x-=d;
	if(!f)
		swap(x,y);
	cout<<x<<" "<<y;
	cout<<"\n";
	return;
}

int main(){
	int TEST;
	cin>>TEST;
	for(int test=1;test<=TEST;test++){
		solve(test);
	}
	return 0;
}