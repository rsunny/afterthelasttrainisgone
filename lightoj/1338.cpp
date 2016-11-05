#include <bits/stdc++.h>

using namespace std;

void solve(int test){
	string s1,s2;
	string temp="";
	getline(cin,temp);
	// cout<<"1 "<<temp<<" ";
	for(int i=0;i<(int)temp.size();i++){
		if(temp[i]==' ')
			continue;
		temp[i]=tolower(temp[i]);
		if(temp[i]>='a' and temp[i]<='z');
			s1+=temp[i];
	}
	// cout<<"1 "<<s1<<" ";
	temp="";
	getline(cin,temp);
	// cout<<"2 "<<temp<<" ";
	for(int i=0;i<(int)temp.size();i++){
		if(temp[i]==' ')
			continue;
		temp[i]=tolower(temp[i]);
		if(temp[i]>='a' and temp[i]<='z');
			s2+=temp[i];
	}
	// cout<<"2 "<<s2<<" ";
	sort(s1.begin(), s1.end());
	sort(s2.begin(), s2.end());
	string res="No";
	if((int)s1.size()==(int)s2.size() and s1==s2)
		res="Yes";
	printf("Case %d: ",test);
	cout<<res<<"\n";
	return ;
}

int main(){
	int TEST;
	scanf("%d",&TEST);
	string temp;
	getline(cin,temp);
	for(int test=1;test<=TEST;test++){
		solve(test);
	}
	return 0;
}