# Cisco Packet Tracer

## Настройка роутера локальной сети

```shell
! перевод CLI в режим ...
enable
configure terminal
hostname Router0

! Интерфейс для Switch1 (ПК)
interface GigabitEthernet0/0
 ip address 192.168.1.1 255.255.255.0
 no shutdown
exit

! Интерфейс для Switch0 (Ноутбуки)
interface GigabitEthernet0/1
 ip address 192.168.2.1 255.255.255.0
 no shutdown
exit

! Интерфейс в сторону "INTERNET"
interface GigabitEthernet0/2
 ip address 10.0.0.1 255.255.255.252
 no shutdown
exit

! маршрут по умолчанию для всего неизвестного трафика
ip route 0.0.0.0 0.0.0.0 10.0.0.2

! Настройка DHCP для подсетей
ip dhcp pool LAN1
 network 192.168.1.0 255.255.255.0
 default-router 192.168.1.1
 dns-server 8.8.8.8
exit

ip dhcp pool LAN2
 network 192.168.2.0 255.255.255.0
 default-router 192.168.2.1
 dns-server 8.8.8.8
exit
```

## Настройка роутера Internet

```bash
enable
configure terminal
hostname INTERNET

! В сторону Router0
interface GigabitEthernet0/0/0
 ip address 10.0.0.2 255.255.255.252
 no shutdown
exit

! В сторону L3 Коммутатора (используем новую подсеть 10.0.0.4/30)
interface GigabitEthernet0/0/1
 ip address 10.0.0.5 255.255.255.252
 no shutdown
exit

! Статический маршрут к ПК (через Router0)
ip route 192.168.0.0 255.255.0.0 10.0.0.1

! Статический маршрут к серверам (через L3 Switch)
ip route 172.16.0.0 255.255.255.0 10.0.0.6
exit
```

## Настройка коммутатора L3 для серверов

```bash
enable
configure terminal
hostname ServerSwitch

! Включаем маршрутизацию
ip routing

! Настройка порта в сторону роутера INTERNET
interface GigabitEthernet1/0/1
 no switchport
 ip address 10.0.0.6 255.255.255.252
 no shutdown
exit

! Настройка VLAN для серверов
vlan 10
 name SERVERS
exit

interface Vlan 10
 ip address 172.16.0.1 255.255.255.0
 no shutdown
exit

! Назначаем порты для серверов (G1/0/2 - 4)
interface range GigabitEthernet1/0/2-4
 switchport mode access
 switchport access vlan 10
 spanning-tree portfast
exit

! Маршрут по умолчанию в сторону интернета
ip route 0.0.0.0 0.0.0.0 10.0.0.5
exit
```

Необходимо вручную прописать IP-адреса (из промежутка 172.16.0.2-172.16.0.254) и шлюзы у серверов. 