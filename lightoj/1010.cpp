#include <bits/stdc++.h>

using namespace std;

int n,m;
int n1,n2,m1,m2;

void solve(){
	scanf("%d %d",&n,&m);
	n2=n/2;
	n1=n-n2;
	m2=m/2;
	m1=m-m2;
	int cnt = (n1*m1) + (n2*m2);
	if(n==1 or m==1) 
		cnt=max(cnt,max(n,m));
	else if(n==2 or m==2){
		n=max(n,m);
		int a = (4*(n/4));
		if(n%4==1)
			a+=2;
		else if(n%4>1)
			a+=4;
		cnt = max(cnt,a);
	}
	printf("%d",cnt);
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