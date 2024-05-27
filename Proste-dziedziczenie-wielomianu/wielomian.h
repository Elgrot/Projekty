//
// Created by emils on 13.12.2022.
//

#ifndef ZADANIE_2A_PROSTE_DZIEDZICZENIE_WIELOMIANU_PRZEZ_JEDNOMIAN_ELGROT_WIELOMIAN_H
#define ZADANIE_2A_PROSTE_DZIEDZICZENIE_WIELOMIANU_PRZEZ_JEDNOMIAN_ELGROT_WIELOMIAN_H
#include <iostream>
#include <string.h>
class Wielomian{
protected:
    int st{};
    float *wsp{};

public:
    Wielomian();
    Wielomian(int s, float *w);
    Wielomian(int s);
    Wielomian(char *n);
    Wielomian(Wielomian &);
    ~Wielomian();
    int Okreslst(const char *n);
    void StworzWielomian(int s);
    char* AnalizujJednomian(char *n);
    void WypelnijWielomian(char *n);
    void Analizuj(const char *n);
    void Pokaz(std::ostream &);

    Wielomian &operator=(const Wielomian &);
    Wielomian &operator*=(float c);
    Wielomian &operator+=(const Wielomian &);
    Wielomian &operator-=(const Wielomian &);
    virtual Wielomian operator/(const Wielomian &);
    Wielomian operator/(float);
    float operator[](int);
    Wielomian operator()(const Wielomian &); // Dzielenie schematem Hornera
    float operator()(float);

    Wielomian pochodna();

    void operator/=(const Wielomian &);
    void operator/=(float);

    friend Wielomian operator*(const Wielomian &, const float &);
    friend Wielomian operator*(const Wielomian &, const Wielomian &);
    friend Wielomian operator+(const Wielomian &, const Wielomian &);
    friend Wielomian operator-(const Wielomian &, const Wielomian &);
    friend std::ostream &operator<<(std::ostream &, Wielomian &);
    friend std::istream &operator>>(std::istream &, Wielomian &w);
};

class Jednomian : public Wielomian{
public:
    Jednomian();
    Jednomian(int s, float w);
};

Jednomian::Jednomian(){
    st = 0;
    wsp = new float[1];
    *wsp = 0;
}

Jednomian::Jednomian(int st, float ws)
{
    this->st = st;

    StworzWielomian(this->st);

    this->wsp[st] = ws;
}

char* UsunSpacje(const char *napis){
    char *napis2 = _strdup(napis);
    char *src = napis2, *dst = napis2;
    while (*src)
        if (*src != ' ')
            *dst++ = *src++;
        else
            src++;
    *dst = 0;
    return napis2;
}

Wielomian::Wielomian(){
    st = 0;
    wsp = new float[1];
    *wsp = 0;
}

Wielomian::Wielomian(int s, float *w){
    st = s;
    StworzWielomian(s);
    for (int i = 0; i < s + 1; i++) {
        this->wsp[i] = w[i];
    }
}

Wielomian::Wielomian(int s){
    st = s;
    wsp = 0;
    StworzWielomian(st);
    for (int i = 0; i < s + 1; i++)
        this->wsp[i] = 0;
}

Wielomian::Wielomian(char *n){
    Analizuj(n);
}

Wielomian::Wielomian(Wielomian &w){
    st = w.st;
    StworzWielomian(st);
    for (int i = 0; i < st + 1; i++)
        this->wsp[i] = w.wsp[i];
}

Wielomian::~Wielomian(){
    delete[] wsp;
}


int Wielomian::Okreslst(const char* napis) {
    int st = 0;
    int nst = 0;
    char* end;

    for (int i = 0; napis[i]; i++) {
        if (napis[i] == 'x') {
            if (napis[i + 1] == '^') {
                nst = strtol(napis + i + 2, &end, 10);
                i = end - napis;
            }
            else
                nst = 1;

            if (nst > st)
                st = nst;
        }
    }
    return st;
}

void Wielomian::StworzWielomian(int s) {
    delete[] wsp;
    this->wsp = new float[s + 1];//(float*)malloc(sizeof(float) * (w->st+1));
    for (int i = 0; i < s + 1; i++)
        this->wsp[i] = 0;
}

char* Wielomian::AnalizujJednomian(char* napis) {
    int znak, wyk;
    float wsp;
    // określamy znak
    if (*napis == '-')
        znak = -1;
    else
        znak = 1;
    if (*napis == '-' || *napis == '+')
        napis++;

    // określamy współczynnik
    if (*napis == 'x')
        wsp = 1;
    else
        wsp = strtof(napis, &napis);

    // określamy wykładnik
    if (*napis != 'x')
        wyk = 0;
    else
    if (*++napis != '^')
        wyk = 1;
    else
        wyk = strtol(++napis, &napis, 10);

    // dopisujemy do wielomianu
    this->wsp[wyk] += wsp * znak;

    return napis;
}

void Wielomian::WypelnijWielomian(char* napis) {
    while (*napis)
        napis = AnalizujJednomian(napis);
}

void Wielomian::Analizuj(const char* napis){
    char* napis2 = UsunSpacje(napis);
    st = Okreslst(napis2);
    StworzWielomian(st);
    WypelnijWielomian(napis2);
}

void Wielomian::Pokaz(std::ostream &o){
    for (int i = 0; i <= this->st; i++) {
        if (wsp[i] == 0)
            continue;
        else if (i == 0)
            if (wsp[i] > 0)
                o << this->wsp[i] << " ";
            else
                o << std::showpos << this->wsp[i] << " ";
        else if (i == 1)
            o << std::showpos << this->wsp[i] << "x ";
        else
            o << std::showpos << this->wsp[i] << "x^" << std::noshowpos << i << " ";
    }
}

Wielomian &Wielomian::operator=(const Wielomian &w){
    /*st = w.st;*/
    if(st < w.st || st > w.st){
        st = w.st;
        float *new_wsp = new float[w.st+1];
        for(int i=0; i<=w.st; i++)
            new_wsp[i] = w.wsp[i];
        wsp = new_wsp;
    }
    // w1 i w2 sa tablicami ktore staja sie na poczatku ostatnimi elementami tablic wsp
    // w kazdym kroku petli zmniejszaja sie o jeden i w1 jest przypisywana wartosc poprzednia az nie dojdziemy do poczatku tablicy
    // int i = w.st-1;
    for(float *w1=wsp+w.st+1, *w2=w.wsp+w.st+1; w1-- > wsp;){
        *w1 = *--w2;
    }
    return *this;
}

Wielomian &Wielomian::operator*=(float c){
    // w staje sie ostatnim elementem tablicy i w kazdym kroku kolejny element jest mnozony przez stala
    // petla konczy sie kiedy w stanie sie pierwszym elementem tablicy
    for(float *w=wsp+st+1; w-- > wsp;)
        *w *= c;
    return *this;
}

Wielomian operator*(const Wielomian &a, const Wielomian &b){
    Wielomian wynik(a.st + b.st);

    for(int i = a.st; i >= 0; i--){
        for(int j = b.st; j >= 0; j--){
            wynik.wsp[i+j] += a.wsp[i] * b.wsp[j];
        }
    }
    return wynik;
}

Wielomian operator*(const Wielomian &w, const float &f){
    Wielomian wynik;
    wynik = w;
    return wynik*=f;
}


Wielomian &Wielomian::operator+=(const Wielomian &w){
    if(st < w.st){
        st = w.st;
        float *new_wsp = new float[w.st+1];
        for(int i=0; i<=w.st; i++)
            new_wsp[i] = w.wsp[i];
        wsp = new_wsp;
    }
    float *w1 = wsp+w.st+1, *w2 = w.wsp+w.st+1;
    while(w1-- > wsp){
        *w1 += *--w2;
    }
    return *this;
}

Wielomian operator+(const Wielomian &a, const Wielomian &b){
    Wielomian w(a.st, a.wsp);
    w += b;
    return w;
}

Wielomian &Wielomian::operator-=(const Wielomian &w){
    if(st < w.st){
        st = w.st;
        float *new_wsp = new float[w.st+1];
        for(int i=0; i<=st; i++)
            new_wsp[i] = wsp[i];
        wsp = new_wsp;
    }
    float *w1 = wsp+w.st+1, *w2 = w.wsp+w.st+1;
    while(w1-- > wsp){
        *w1 -= *--w2;
    }
    return *this;
}

Wielomian operator-(const Wielomian &a, const Wielomian &b){
    Wielomian w(a.st, a.wsp);
    w -= b;
    return w;
}

Wielomian Wielomian::operator/(const Wielomian &dzielnik){
    if(dzielnik.st == 0 && dzielnik.wsp[0] == 0)
        throw std::invalid_argument("Nie można dzielić przez zero!");
    Wielomian wynik,dzielna;
    dzielna=*this;
    wynik.st = st-dzielnik.st;
    if(wynik.st>=0){
        wynik.StworzWielomian(wynik.st);
        for(int i=wynik.st; i>=0; i--){
            Jednomian jedn(i, dzielna.wsp[i+dzielnik.st]/dzielnik.wsp[dzielnik.st]);
            wynik+=jedn;
            dzielna-=jedn*dzielnik;
        }
        return wynik;
    }
    return *this;
}

Wielomian Wielomian::operator/(float k){
    if(k == 0)
        throw std::invalid_argument("Nie można dzielić przez zero!");
    Wielomian wynik;
    wynik=*this;
    for(int i=0; i<=st; i++){
        wynik.wsp[i]/=k;
    }
    return wynik;
}

void Wielomian::operator/=(const Wielomian &dzielnik){
    *this = *this / dzielnik;
}

void Wielomian::operator/=(float k){
    *this = *this / k;
}

float Wielomian::operator[](int s) {
    if (s > st)
        throw std::invalid_argument("Za duży stopień!");
    else
        return wsp[s];
}

Wielomian Wielomian::operator()(const Wielomian &w){
    Wielomian wynik(st-1);
    float reszta=0,zero = w.wsp[0]*(-1); // reszta dzielenia, miejsce zerowe wielomianu w
    int j=st; // iterowanie po wspolczynnikach *this
    wynik.wsp[wynik.st] = wsp[j--]; // przepisanie najwiekszej wartosci *this do wyniku

    for(int i = wynik.st; i >= 0; i--) {
        if(i == 0)
            reszta = zero * wynik.wsp[i] + wsp[j--];
        else {
            wynik.wsp[i-1] = zero * wynik.wsp[i] + wsp[j--];
        }
    }
    if(reszta != 0)
        std::cout << "\nReszta schematu Hornera: " << reszta;
    return wynik;
}

float Wielomian::operator()(float p) {
    float wynik = wsp[0];
    for(int i = 1; i<=st; i++){
        wynik += wsp[i]*p;
    }
    return wynik;
}
/* 2x^3+2x^2+2x^1+2x^0 -pochodna--> 6x^2+4x^1+2x^0*/
Wielomian Wielomian::pochodna(){
    Wielomian poch;
    poch.StworzWielomian(st-1);
    poch.st = st-1;
    int s = st;
    for(float *w1=wsp+st+1, *w2=poch.wsp+poch.st+1; w1-- > wsp;)
        *--w2 = *w1 * s--;
    return poch;
}

std::ostream& operator<<(std::ostream &o, Wielomian &w) {
    w.Pokaz(o);
    return o;
}

std::istream &operator>>(std::istream &i, Wielomian &w) {
    char c[1024];
    i.getline(c,sizeof(c));
    w.Analizuj(c);
    return i;
}
#endif //ZADANIE_2A_PROSTE_DZIEDZICZENIE_WIELOMIANU_PRZEZ_JEDNOMIAN_ELGROT_WIELOMIAN_H
