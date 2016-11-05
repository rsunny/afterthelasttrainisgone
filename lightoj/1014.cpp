#include <bits/stdc++.h>

using namespace std;

void solve(int test){
	long long int p,l;
	scanf("%lld %lld",&p,&l);
	printf("Case %d: ",test);	
	long long int avail=p-l;
	int flag=0;
	set<long long int> all;
	long long int i=1;
	while(1){
		long long int lmt=i*i;
		if(lmt>avail)
			break;
		if(avail%i){
			i++;
			continue;
		}
		long long int a=i;
		long long int b=avail/i;
		if(a>l)
			flag=1,all.insert(a);
		if(b>l)
			flag=1,all.insert(b);
		i++;
	}
	if(!flag){
		printf("impossible");
		printf("\n");
		return;
	}
	set<long long int> :: iterator it;
	int j=all.size()-1;
	for(it=all.begin();it!=all.end();it++){
		printf("%lld",*it);
		if(j)
			printf(" ");
		j--;
	}
	printf("\n");
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