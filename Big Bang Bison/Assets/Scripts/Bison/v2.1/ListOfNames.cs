using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListOfNames : MonoBehaviour
{
    public string[] names = 
    {
        "Samuel", "Grant", "Chase", "Colin", "Griffin", "Caetano", "Max", "Lu", "Carlos", "Warner", "Erika", 
        "Chad", "Brad", "Johnny", "Destiny", "Ariana", "Taylor", "Demi", "Denise", "Joey", "Susan", "Penelope",
        "Ed", "Tad", "Robin", "Marcelo", "Nathan", "Elin", "Nick", "Izzy", "Kevin", "Jared", "Chris", "Logan",
        "Monica", "Christine", "Jenny", "Gabby", "Mary", "Ash", "Zane", "Yolanda", "Jorge", "Jeff", "Tim", "Noah",
        "Erin", "Bunny", "Jim", "Elizabeth", "Zach", "Cody", "Drake", "Josh", "Megan", "Freddie", "Felicity",
        "Faith", "Grace", "Hope", "Jason", "Maggy", "Ariel", "Vella", "Lyn", "Belle", "Arisha", "Eira", "Fiona",
        "Cynthia", "Gary", "Ethan", "Judas", "Peter", "James", "John", "Andrew", "Bartholomew", "Jude", "Matthew",
        "Thomas", "Simon", "Fredward", "Cat", "George", "Beth", "Sophia", "Rosalina", "Luma", "Pikachu", "Tauros",
        "Mewtwo", "Bulbasaur", "Squirtle", "Charizard", "Yoshi", "Sonic", "Mario", "Luigi", "Hannah", "Leena",
        "Hailey", "Katie", "Michael", "Michelle", "Theodore", "Audrey", "Zeus", "Hades", "Poseidon", "Aphrodite", 
        "Hera", "Hermes", "Athena", "Apollo", "Demeter", "Artemis", "Hephaestus", "Ares", "Dionysus", "Percy", 
        "Jackson", "Annabeth", "Grover", "Leo", "Harry", "Ron", "Hermione", "Appa", "Yip Yip", "Aang", "Katara",
        "Socka", "Toph", "Zuko", "Uncle Iroh", "Azula", "Karen"
    };

    
    public int GetLength()
    {
        Debug.Log("list of names is " + names.Length + " names long");
        return names.Length;
    }
}
