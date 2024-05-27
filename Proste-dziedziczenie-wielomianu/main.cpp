//
// Created by emils on 13.12.2022.
//
#include "wielomian.h"


int main(){
    float ws[4] = {-15 ,5, -3, 1}, H = 0;
    char *c = (char*)"x-3", *v = (char*)"1x^2+1x+1";
    Wielomian w(3, ws);
    Wielomian y(v);
    Wielomian k(c);
    Wielomian l;
    Wielomian a;
    Wielomian d = y;

    std::cout << "Wielomian w(stopien, wspolczynniki): " << w << "\n";
    std::cout << "Wielomian y(lancuch znakow): " << y << "\n";
    std::cout << "Wielomian k(lancuch znakow): " << k << "\n";
    std::cout << "Wielomian l(pusty): " << l << "\n";
    std::cout << "Wielomian a(pusty): " << a << "\n";
    std::cout << "Wielomian d = y: " << d << "\n";
    l = w.pochodna();
    std::cout << "Pochodna wielomianu w = w' -> " << l << "\n";
    a = w+y;
    std::cout << "w + y -> " << a << "\n";
    w += y;
    std::cout << "w += y -> " << w << "\n";
    a = w-y;
    std::cout << "w - y -> " << a << "\n";
    w -= y;
    std::cout << "w -= y -> " << w << "\n";
    a = k*y;
    std::cout << "k * y -> " << a << "\n";
    w *= 2;
    std::cout << "w *= 2 -> " << w << "\n";
    a = w(k);
    std::cout << "Dzielenie schematem Hornera w(k): " << a << "\n";
    a = w / k;
    std::cout << "w / k -> " << a << "\n";
    w /= k;
    std::cout << "w /= k -> " << w << "\n";
    a = w / 2;
    std::cout << "w / 2 -> " << a << "\n";
    w /= 2;
    std::cout << "w /= 2 -> " << w << "\n";
    H = w[2];
    std::cout << "w[2] -> " << H << "\n";
    H = k(6);
    std::cout <<"Wielomian k w punkcie 6 = k(6) -> " << H << "\n";
    std::cout << "Wpisz wielomian:";
    std::cin >> l;
    std::cout << "Wielomian l wczytany z wejscia: " << l << "\n";
}