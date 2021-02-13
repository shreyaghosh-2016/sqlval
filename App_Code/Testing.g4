grammar Testing;
@parser::members
{
    protected const int EOF = Eof;
}
 
@lexer::members
{
    protected const int EOF = Eof;
    protected const int HIDDEN = Hidden;
}
/*
 * Parser Rules
 */
gram: (init)+ (startc) ;

startc : (sel|ins|init|drop)*;    //| ins (init|drop|ins)* | drop (ins|init|sel)*;

init: CR TAB ID  '(' exp ')' SC  #start;
drop : 'DROP' TAB ID SC #dropt;
exp:  ID TYPE1			#idnc1
	| ID TYPE2 '(' A ')' #idnc2
	
	| ID TYPE2 '(' NUM ')' #idnc2c  //VARCHAR
	| ID TYPE3 #idnc3
	| ID TYPE3 '(' A ')' #idnc3a //char
	| ID TYPE3 '(' NUM ')' #idnc3c //char
	| exp COMMA ID TYPE1 #ridnc1
	| exp COMMA ID TYPE2 '(' A ')'  #ridnc2
	| exp COMMA ID TYPE2 '(' NUM ')'  #ridnc2c
	| exp COMMA ID TYPE3 #idnc3
	| exp COMMA ID TYPE3 '(' A ')'  #idnc3a
	| exp COMMA ID TYPE3 '(' NUM ')'  #idnc3c

	| ID TYPE1 CONSTRAINT1 #idc11
	| ID TYPE2 '(' A ')' CONSTRAINT1 #idc12  //VARCHAR
	| ID TYPE2 '(' NUM ')' CONSTRAINT1 #idc12c  //VARCHAR
	| ID TYPE3 CONSTRAINT1 #idc13
	| ID TYPE3 '(' A ')' CONSTRAINT1 #idc13a  //CHAR
	| ID TYPE3 '(' NUM ')' CONSTRAINT1 #idc13c  //CHAR
	
	| ID TYPE1 CONSTRAINT2 ID '(' ID ')' #idc21  //constraints 
	| ID TYPE2 '(' A ')'CONSTRAINT2 ID '(' ID ')' #idc22 //VARCHAR
	| ID TYPE2 '(' NUM ')'CONSTRAINT2 ID '(' ID ')' #idc22c
	| ID TYPE3 CONSTRAINT2 #idc23
	| ID TYPE3 '(' A ')' CONSTRAINT2 #idc23a  //CHAR
	| ID TYPE3 '(' NUM ')' CONSTRAINT2 #idc23c  //CHAR
	
	 
	| ID TYPE1 CH '(' ID REL NUM ')' #idcht1
	| ID TYPE2 '(' A ')'  CH '(' ID REL '\''ID '\'' ')' #idch //VARCHAR
	| ID TYPE2 '(' NUM ')'  CH '(' ID REL '\''ID '\''')' #idchc
	| ID TYPE3 CH '(' ID REL '\''ID '\'' ')' #idcht3
	| ID TYPE3 '(' A ')'  CH '(' ID REL '\''ID '\'' ')' #idch3a //CHAR
	| ID TYPE3 '(' NUM ')'  CH '(' ID REL '\''ID '\''')' #idch3c//char

	| ID TYPE1 DF NUM #iddf1
	| ID TYPE2 '(' A ')' DF '\''ID'\'' #iddf2  //VARCHAR
	| ID TYPE2 '(' NUM ')' DF '\''ID'\'' #iddf2c
	| ID TYPE3 DF '\''ID'\'' #iddf3
	| ID TYPE3 '(' A ')' DF '\''ID'\'' #iddf3a  //CHAR
	| ID TYPE3 '(' NUM ')' DF '\''ID'\'' #iddf3c

	| exp COMMA ID TYPE1 CONSTRAINT1 #idc11
	| exp COMMA ID TYPE2 '(' A ')' CONSTRAINT1 #idc12  
	| exp COMMA ID TYPE2 '(' NUM ')' CONSTRAINT1 #idc12
	| exp COMMA ID TYPE3 CONSTRAINT1 #idc13
	| exp COMMA ID TYPE3 '(' A ')' CONSTRAINT1 #idc13a  //CHAR
	| exp COMMA ID TYPE3 '(' NUM ')' CONSTRAINT1 #idc13c  //CHAR

	| exp COMMA ID TYPE1 CONSTRAINT2 ID '(' ID ')'  #idc21
	| exp COMMA ID TYPE2 '('A ')'  CONSTRAINT2 ID '(' ID ')' #idc22
	| exp COMMA ID TYPE2 '(' NUM ')'  CONSTRAINT2 ID#idc22
	| exp COMMA ID TYPE3 CONSTRAINT2 #idc23
	| exp COMMA ID TYPE3 '(' A ')' CONSTRAINT2 #idc23a  //CHAR
	| exp COMMA ID TYPE3 '(' NUM ')' CONSTRAINT2 #idc23c  //CHAR

	| exp COMMA ID TYPE1 CH '(' ID REL NUM ')' #idcht1
	| exp COMMA ID TYPE2 '(' A ')' CH '(' ID REL ID ')' #idch
    | exp COMMA ID TYPE2 '(' NUM ')' CH '(' ID REL ID ')' #idch
	| exp COMMA ID TYPE3 CH '(' ID REL '\''ID '\'' ')' #idcht3
	| exp COMMA ID TYPE3 '(' A ')'  CH '(' ID REL '\''ID '\'' ')' #idch3a //CHAR
	| exp COMMA ID TYPE3 '(' NUM ')'  CH '(' ID REL '\''ID '\''')' #idch3c//char

	| exp COMMA ID TYPE1 DF NUM #iddf1
	| exp COMMA ID TYPE2 '(' A ')' DF ID #iddf2
	| exp COMMA ID TYPE2 '(' NUM ')' DF ID #iddf2
	| exp COMMA ID TYPE3 DF '\''ID'\'' #iddf3
	| exp COMMA ID TYPE3 '(' A ')' DF '\''ID'\'' #iddf3a  //CHAR
	| exp COMMA ID TYPE3 '(' NUM ')' DF '\''ID'\'' #iddf3c;

sel: SL '*' FROM ID SC #selwarn
    | selexp SC #selstart1
	| selexp WHERE ID REL ID SC #selstart2 
	| selexp WHERE ID REL NUM SC #selstart3;


selexp:SL colsel FROM tabsel #selsec;
colsel:  'COUNT' '(' ID ')' #sid
        | 'AVG' '(' ID  ')' #sid1
		| 'MAX' '(' ID ')' #sid1 //have to write for sid1
		| 'MIN' '(' ID ')' #sid1  
        | ID #sid
        | AVG '(' ID ')' #sid
		| colsel COMMA ID #sid;
tabsel: ID #tid1
		| tabsel COMMA ID #tid;
ins: INSERT INTO ID '(' insexp1 ')' VALUES '(' insexp2 ')' SC #instart;
insexp1: ID  #inid1
		| insexp1 COMMA ID #inid1;  
insexp2: '\''ID'\'' #inid2
		|'\''NUM'\'' #inum
		| insexp2 COMMA '\''ID'\'' #inid21 
		| insexp2 COMMA '\''NUM'\'' #inum1;
/*
 * Lexer Rules
 */

CR : 'CREATE' | 'create' ;
SL : 'SELECT' | 'select' ;
TAB : 'TABLE' | 'table';
WHERE: 'WHERE' | 'where';
TYPE1: 'int'| 'INT';
TYPE2: 'varchar' | 'VARCHAR' ;  //limit modify
TYPE3: 'char' | 'CHAR';
CONSTRAINT1: 'NOT NULL' | 'UNIQUE' | 'PRIMARY KEY';
CONSTRAINT2: 'FOREIGN KEY REFERENCES' | 'REFERENCES' | 'references' | 'foreign key references' ;   //circular references to be done by it 
CH: 'CHECK'|'check';
DF: 'DEFAULT'|'default';
INSERT: 'INSERT' | 'insert';
INTO: 'INTO'|'into';
VALUES:'VALUES'|'values' ;
FROM: 'FROM'|'from';
NUM: ('0'..'9')+;
SC: ';';
REL: '>' | '<' | '>=' | '<=' | '=';
 
COMMA: ',';
A : 'MAX'   ;
ID : ('a'..'z'|'A'..'Z'|'_') ('a'..'z'|'A'..'Z'|'0'..'9'|'_')* ;
WS	:   (' ' | '\r' | '\n') -> skip;