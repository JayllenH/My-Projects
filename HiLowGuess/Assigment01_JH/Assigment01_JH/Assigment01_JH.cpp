// Assigment01_JH.cpp : This file contains the 'main' function. Program execution begins and ends there.
// 
// Jayllen H
// IGME 309
// Assignment 1
//

#include <iostream>
#include <cstdlib>

int main()
{
    char playAgainResponse = 'n';
    int randomNumber = rand() % 100 + 1;
    do 
    {
        randomNumber = rand() % 100 + 1;
        int guess;
        int numbOfGuesses = 0;
 
        //print out question and get response
        std::cout << "Welcome to the Hi-Low Guessing Game!\nPlease Guess a number between 1 and 100\n\n";

        //do while loop 
        do
        {
            std::cout << "Enter a number or -1 to quit: ";
            std::cin >> guess;
            numbOfGuesses++;

            //checks if quit
            if (guess == -1)
                return 0;

            //check is number is valid and then checks hi-low
            if (guess < 0 || guess > 100)
                std::cout << "Your guess is invalid, Try again!\n";
            else 
                if (guess < randomNumber)
                std::cout << "Your guess is low.\n";
            else 
                if (guess > randomNumber)
                 std::cout << "Your guess is high.\n";

        } while (guess != randomNumber);

        //print statement and number of guesses
        std::cout << "Your guess is correct! The number of guesses is " << numbOfGuesses << ".\n Do you want to play again? {y/n} ";
        std::cin >> playAgainResponse;
        std::cout << "\n";
    } while (playAgainResponse != 'n');
    
   

   
}


