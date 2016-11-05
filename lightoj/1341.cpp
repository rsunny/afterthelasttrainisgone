#include <bits/stdc++.h>

using namespace std;

#define MAX_N 1000001

int isprime[MAX_N];
vector<int> pr;

void pre(){
    memset(isprime,0,sizeof isprime);
    
    for(int i=2;i<MAX_N;i++){
        if(isprime[i]==0){
            isprime[i]=i;
            pr.push_back(i);
        }
        for(int j=0;j<(int)pr.size() and pr[j]<=isprime[i] and i*pr[j]<MAX_N;j++)
            isprime[i*pr[j]] = pr[j];
    }
}

vector<int> val;
vector<int> ct;
long long int a,b;
long long int srt;
set<int> fact;


void doit(int num,int pos){
	//cout<<num<<" "<<pos<<"\n";
	if(num>=b)
		return ;
	fact.insert(num);//,cout<<num<<" ";
	if(pos==(int)val.size())
		return ;
	doit(num,pos+1);
	for(int i=0;i<ct[pos];i++){
		if(num*val[pos]<b){
			num*=val[pos];
			doit(num,pos+1);
		}
		else
			break;
	}
	return ;
}

void solve(){
	val.clear();
	ct.clear();
	fact.clear();

	scanf("%lld %lld",&a,&b);

	long long int tot_fact=1;
	srt = sqrt(a);

	if(srt*srt==a and b==srt){
		cout<<0;
		return ;
	}
	if(srt<b){
		cout<<0;
		return ;
	}

	for(int i=0;i<(int)pr.size();i++){
		int cnt=0;
		if(pr[i]>(int)srt)
			break;
		while(a%pr[i]==0){
			a/=pr[i];
			cnt++;	
		}
		if(cnt)
			val.push_back(pr[i]),ct.push_back(cnt);
		tot_fact*=(cnt+1);
		if(a==1)
			break;
	}
	if(a>1)
		tot_fact*=2;
	tot_fact/=2;

	doit(1,0);
	cout<<tot_fact-fact.size();
	return;
}

int main(){
	pre();
	int TEST;
	cin>>TEST;
	for(int test=1;test<=TEST;test++){
		printf("Case %d: ",test);
		solve();
		printf("\n");
	}
	return 0;
}