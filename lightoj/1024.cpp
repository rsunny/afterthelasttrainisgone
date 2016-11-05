#include <bits/stdc++.h>

using namespace std;

map<int,int> cnt;

void primeFactors(int n){
	int k=0;
    while (n%2 == 0){
        n = n/2;
        k++;
    }
    cnt[2]=max(cnt[2],k);
    for (int i=3;i<=sqrt(n);i+=2){
        k=0;
        while (n%i == 0){
            n = n/i;
            k++;
        }
        cnt[i]=max(cnt[i],k);
    }
 
    if (n > 2){
    	cnt[n]=max(cnt[n],1);
    }
    return ;
}

string multiply( string a, long long b ) {
    int carry = 0;
    for( int i = 0; i < (int)a.size(); i++ ) {
        carry += (a[i] - 48) * b;
        a[i] = ( carry % 10 + 48 );
        carry /= 10;
    }
    while( carry ) {
        a += ( carry % 10 + 48 );
        carry /= 10;
    }
    return a;
}

int n,num;
string ans;

void solve(int test){
	scanf("%d",&n);
	for(int i=0;i<n;i++){
		scanf("%d",&num);
		primeFactors(num);
	}
	ans="1";
	for(map<int,int> :: iterator it=cnt.begin();it!=cnt.end();it++){
		int b=it->first;
		int p=it->second;
		for(int i=0;i<p;i++)
			ans=multiply(ans,b);
		cnt[it->first]=0;
	}
	printf("Case %d: ",test);
	for(int i=ans.size()-1;i>=0;i--)
		printf("%c",ans[i]);
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