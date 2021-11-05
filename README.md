# DuConsole

CMD, Powershell 스크립트를 편하게 실행하려고 만들었다.
입력은 안되고 출력만 받음.... 입력은 만들기 귀찮슴미다...

## 스크립트 구조
```
[설정값]
[한줄띄고]
[스크립트본문]
```

확장자는 .duc 또는 .duconsole 이어야 함. 인코딩은 무조건 UTF8....밖에 못읽음

## 설정값
* name=문자열
> 이름 지정
* type=[cmd|ps]
> cmd는 cmd.exe, ps는 powershell.exe 사용
* start=[true|false]
> 시작하자마자 스크립트 실행 여부
* runas=[true|false]
> Administrator로 실행 여부
* autoexit=[true|false]
> 한번 실행하고 종료할때 설정

## cmd 샘플
```
name="THIS IS CMD TEST"
type=cmd
start=false
runas=true
autoexit=false

@echo off

echo 이 것은 첫번째줄
echo 이것은 두번째줄
echo .
echo 오 달링
echo 내사랑 마이러뷰
echo 얼롱 
echo 롱리타임!!!!
echo .
echo 암튼 이건 ANSI/DBCS로 바뀌는듯
```

## Powershell 샘플
```
name=이건 파워쉘 테스트
type=ps
start=false
runas=false
autoexit=false

# test

' 이 것은 첫번째줄'
' 이것은 두번째줄'
' .'
' 오 달링'
' 내사랑 마이러뷰'
' 얼롱 '
' 롱리타임!!!!'
```
