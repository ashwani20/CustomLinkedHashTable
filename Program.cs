/*
 * CSCI 641 Project 1
 * @author  Ashwani Kumar (ak8647)
 */

using System;
using System.Collections;
using System.Collections.Generic;

namespace CSCI641_Lab1
{
    /// <summary>
    /// An exception used to indicate a problem with how
    /// a HashTable instance is being accessed
    /// </summary>
    public class NonExistentKey<Key> : Exception
    {
        /// <summary>
        /// The key that caused this exception to be raised
        /// </summary>
        public Key BadKey { get; private set; }

        /// <summary>
        /// Create a new instance and save the key that
        /// caused the problem.
        /// </summary>
        /// <param name="k">
        /// The key that was not found in the hash table
        /// </param>
        public NonExistentKey(Key k) :
            base("Non existent key in HashTable: " + k)
        {
            BadKey = k;
        }

    }

    /// <summary>
    /// An associative (key-value) data structure.
    /// A given key may not appear more than once in the table,
    /// but multiple keys may have the same value associated with them.
    /// Tables are assumed to be of limited size are expected to automatically
    /// expand if too many entries are put in them.
    /// </summary>
    /// <param name="Key">the types of the table's keys (uses Equals())</param>
    /// <param name="Value">the types of the table's values</param>
    
    
    interface Table<Key, Value> : IEnumerable<Key>
    {
        /// <summary>
        /// Add a new entry in the hash table. If an entry with the
        /// given key already exists, it is replaced without error.
        /// put() always succeeds.
        /// (Details left to implementing classes.)
        /// </summary>
        /// <param name="k">the key for the new or existing entry</param>
        /// <param name="v">the (new) value for the key</param>
        void Put(Key k, Value v);

        /// <summary>
        /// Does an entry with the given key exist?
        /// </summary>
        /// <param name="k">the key being sought</param>
        /// <returns>true iff the key exists in the table</returns>
        bool Contains(Key k);

        /// <summary>
        /// Fetch the value associated with the given key.
        /// </summary>
        /// <param name="k">The key to be looked up in the table</param>
        /// <returns>the value associated with the given key</returns>
        /// <exception cref="NonExistentKey">if Contains(key) is false</exception>
        Value Get(Key k);
    }

    /// <summary>
    /// A class which follows factory pattern and creates objects of 
    /// class - LinkedHashTable. 
    /// </summary>

    class TableFactory
    {
        /// <summary>
        /// Create a Table.
        /// (The student is to put a line of code in this method corresponding to
        /// the name of the Table implementor s/he has designed.)
        /// </summary>
        /// <param name="K">the key type</param>
        /// <param name="V">the value type</param>
        /// <param name="capacity">The initial maximum size of the table</param>
        /// <param name="loadThreshold">
        /// The fraction of the table's capacity that when
        /// reached will cause a rebuild of the table to a 50% larger size
        /// </param>
        /// <returns>A new instance of Table</returns>
        public static Table<K, V> Make<K, V>(int capacity = 100, double loadThreshold = 0.75)
        {
            return new LinkedHashTable<K, V>(capacity, loadThreshold);
        }
    }

    /// <summary>
    /// A class for testing the working of class LinkedHashTable and its object and 
    /// behaviour. It test the object using various generic types.
    /// </summary>
    class TestTable
    {
        public void Test()
        {
            char testToRun = '0';
            do
            {
                Console.Write("Select Test to Run (1-6, 0 to exit): ");
                do
                {
                    testToRun = (char)Console.Read();
                } while (testToRun == '\n' || testToRun == '\r');
                switch (testToRun)
                {
                    case '1':
                        Test1();
                        break;
                    case '2':
                        Test2();
                        break;
                    case '3':
                        Test3();
                        break;
                    case '4':
                        Test4();
                        break;
                    case '5':
                        Test5();
                        break;
                    case '6':
                        Test6();
                        break;
                }
            } while (testToRun != '0');
        }


        private void Test1()
        {
            Console.WriteLine("This test case is not written by me. Prof Brown owns this. This has" +
                " general test cases");
            Table<String, String> ht = TableFactory.Make<String, String>(4, 0.5);
            ht.Put("Joe", "Doe");
            ht.Put("Jane", "Brain");
            ht.Put("Chris", "Swiss");
            try
            {
                foreach (String first in ht)
                {
                    Console.WriteLine(first + " -> " + ht.Get(first));
                }
                Console.WriteLine("=========================");

                ht.Put("Wavy", "Gravy");
                ht.Put("Chris", "Bliss");
                foreach (String first in ht)
                {
                    Console.WriteLine(first + " -> " + ht.Get(first));
                }
                Console.WriteLine("=========================");

                Console.Write("Jane -> ");
                Console.WriteLine(ht.Get("Jane"));
                Console.Write("John -> ");
                Console.WriteLine(ht.Get("John"));
            }
            catch (NonExistentKey<String> nek)
            {
                Console.WriteLine(nek.Message);
                Console.WriteLine(nek.StackTrace);
            }
            Console.WriteLine("TestCase completed");
        }

        private void Test2()
        {
            try
            {

                Console.WriteLine("This test case checks if a key is present in the hastTable or not. "+
                    "Throws NonExistentKey exception if an exception occurs in Get method");
                Table<string, int> myHashTable = TableFactory.Make<string, int>(10, 0.5);
                myHashTable.Put("1234", 1234);
                myHashTable.Put("4321", 4321);
                myHashTable.Put("4321", 1234);

                myHashTable.Put("Goku", 1234);
                myHashTable.Put("Saiyan", 1234);

                foreach (string key in myHashTable)
                {
                    Console.WriteLine(key + " -> " + myHashTable.Get(key));
                }

                Console.WriteLine("Check value of key - Saiyan in hashTable");
                Console.WriteLine(myHashTable.Get("Saiyan"));
                myHashTable.Get("Husky");

            }
            catch (NonExistentKey<String> nek)
            {
                Console.WriteLine(nek.Message);
                Console.WriteLine(nek.StackTrace);
            }
            Console.WriteLine("TestCase completed");

        }
        /// <summary>
        /// This test case has been shared with Prakash Mishra (pm1739) and Devavrat Kalam(dk2792).
        /// </summary>
        public void Test3() {
            Console.WriteLine("This test case checkes performance of my hashtable. " +
                "It also feeds hashtable with size having negative values and loadThreshold ");
            DateTime start = DateTime.Now;
            Console.WriteLine("Testing hashTable with threshold of 0.633 and start capacity" +
                " of 10");
            Table<int, int> myHashTable1 = TableFactory.Make<int, int>(10, 0.633);
            Console.WriteLine("Inserting 10 million entries into hashtable");
            for (int i = 0; i < 10000000; i++)
            {
                myHashTable1.Put(i, i);
            }
            Console.WriteLine("Enumerating throught each entries in HashTable");
            foreach (int x in myHashTable1)
                myHashTable1.Get(x);
            DateTime end = DateTime.Now;
            Console.WriteLine("Time taken for execution: " + (end-start) + "\n");


            Console.WriteLine("Test case 2");
            start = DateTime.Now;

            Console.WriteLine("Testing hashTable with threshold of 0.56 and start capacity" +
                " of 1");

            myHashTable1 = TableFactory.Make<int, int>(1, 0.56);
            Console.WriteLine("Inserting 10 million entries into hashtable");
            for (int i = 0; i < 10000000; i++)
            {
                myHashTable1.Put(i, i);
            }

            Console.WriteLine("Enumerating throught each entries in HashTable");
            foreach (int x in myHashTable1)
            { 
                myHashTable1.Get(x);
            }
            end = DateTime.Now;
            Console.WriteLine("Time taken for execution: " + (end - start) + "\n");


            Console.WriteLine("Test case 3");
            start = DateTime.Now;

            Console.WriteLine("Testing hashTable with threshold of -10 and start capacity " +
                " of -0.43333");

            myHashTable1 = TableFactory.Make<int, int>(-10, -0.43333);
            Console.WriteLine("Inserting 10 million entries into hashtable");
            for (int i = 0; i < 10000000; i++)
            {
                myHashTable1.Put(i, i);
            }

            Console.WriteLine("Enumerating throught each entries in HashTable");
            foreach (int x in myHashTable1)
            {
                myHashTable1.Get(x);
            }
            end = DateTime.Now;
            Console.WriteLine("Time taken for execution: " + (end - start) + "\n");

            Console.WriteLine("TestCase completed");
        }

        /// <summary>
        /// This test case has been taken from Prakash Mishra (pm1739). I don't own this test case.
        /// </summary>
        private void Test4()
        {
            Table<String, String> ht = TableFactory.Make<String, String>(6, 0.5);
            Console.WriteLine("This is to make sure that rehashing works properly");
            ht.Put("Betty", "boop");
            ht.Put("Buggs", "Bunny");
            ht.Put("Charlie", "Brown");
            ht.Put("Duffy", "Duck");
            ht.Put("Dennis", "The Mennace");
            ht.Put("Donald", "Duck");
            ht.Put("Garfield", "MickeyMouse");
            ht.Put("Olive", "Oyl");
            ht.Put("Popeye", "Tailorman");
            ht.Put("Powerpuff", "Girls");
            ht.Put("Road", "Runner");
            ht.Put("Scooby", "Doo");
            ht.Put("Snoopy", "Dog");
            ht.Put("Ninja", "Turtle");
            ht.Put("The", "Simpsons");
            ht.Put("Tom", "Jerry");
            ht.Put("Yogi", "Bear");

            try
            {

                foreach (String first in ht)
                {
                    Console.WriteLine(first + " -> " + ht.Get(first));
                }
                Console.WriteLine("=========================");

            }
            catch (NonExistentKey<String> nek)
            {
                Console.WriteLine(nek.Message);
                Console.WriteLine(nek.StackTrace);
            }
            Console.WriteLine("TestCase completed");

        }

        /// <summary>
        /// This test demonstrates how the hashtable handles null keys and null values.
        /// </summary>
        public void Test5()
        {
            Table<string, string> myHashTable = TableFactory.Make<string, string>(4, 0.5);
            Console.WriteLine("This test will check null keys and null values.");

            try 
            {
                Console.WriteLine("\nAdding null as key and value");
                myHashTable.Put(null, null);

                Console.WriteLine("\nCalling Contains function with null parameter");
                myHashTable.Contains(null);


                Console.WriteLine("\nAdding a key with a null value in the hashtable: ");
                myHashTable.Put("Amy", null);
                Console.WriteLine("Fetching key with null");
                Console.WriteLine("Amy : " + myHashTable.Get("Amy"));

                Console.WriteLine("TestCase completed");

            }

            catch (ArgumentNullException ane)
            {
                Console.WriteLine(ane.Message);
            }
        }

        public void Test6()
        {
            Table<int, int> myHashTable = TableFactory.Make<int, int>(10, 0.45);
            Console.WriteLine("This test case test chaining");
            
            for (int i=0; i<1000; i+= 10)
            {
                myHashTable.Put(i, i);
            }
            
            Console.WriteLine("Enumerating through the hashtable");

            foreach (int key in myHashTable)
            {
                Console.WriteLine(key + " : " + myHashTable.Get(key));
            }

            Console.WriteLine("TestCase completed");
        }
    }

        

    /// <summary>
    /// The main class which the entry point main function. Creates an object 
    /// of TestTable class and runs the test cases.
    /// </summary>
    class MainClass
    {
        public static void Main(string[] args)
        {
            TestTable testObj = new TestTable();
            testObj.Test();

            Console.ReadLine();
        }
    }

    /// <summary>
    /// The class using which implements the interface Table. It has a hashtable which resizes itself 
    /// when it's capacity reaches a threshold value. The hashtable stores object of another generic 
    /// class Node which has key, value and next as its data members. The constructor of this class
    /// creates a new hashtable using the parameters. There are functions for Get, Put and Contains
    /// which are similar to how a HashTable works. It also implements IEnumerable interface for
    /// enumerating through the LinkedHashTable object.
    /// </summary>
    /// <typeparam name="K">Key for hashtable</typeparam>
    /// <typeparam name="V">Value corresponding to a key</typeparam>
    class LinkedHashTable<K, V> : Table<K, V>
    {
        private Node<K, V>[] hashTable = null;
        private double loadThreshold;
        private int currentCount;

        public int CurrentCount { get => currentCount; set => currentCount = value; }

        /// <summary>
        /// LinkedHashTable constructor which takes capacity and loadThreshold as paramter
        /// and creates a hashtable array of Class Node object.
        /// </summary>
        /// <param name="capacity">Capacity of hashtable</param>
        /// <param name="loadThreshold">Threshold for rehashing</param>
        public LinkedHashTable(int capacity, double loadThreshold)
        {
            if (capacity < 1)
            {
                Console.WriteLine("Capacity is negative so setting it to a default" +
                    "value of 100");
                capacity = 100;
            }

            if (loadThreshold <= 0 || loadThreshold > 1)
            {
                Console.WriteLine("loadThreshold is less than equal to 0 or greater than 1 " +
                    "so setting it to a default value of 0.7");
                loadThreshold = 0.7;
            }
            CurrentCount = 0;
            hashTable = new Node<K, V>[capacity];
            this.loadThreshold = loadThreshold;
        }

        /// <summary>
        /// Calls GetHashCode, takes it absolute value and divides it with the length of 
        /// hashtable object. Returns this new value.
        /// </summary>
        /// <param name="key">Key of hashtable</param>
        /// <returns></returns>
        public int CalculateHashCode(K key) {
            int hashCode = Math.Abs(key.GetHashCode());
            return hashCode % hashTable.Length;
        }

        /// <summary>
        /// Uses Get method to get the value of the key. Get method returns the value of the key,
        /// if key is present in the hashtable. Otherwise, it returns a default value which is 
        /// generic specific. Throws NonExistentKey expection in case a default generic value
        /// is returned. Otherwise, it returns true.
        /// </summary>
        /// <param name="k">key to be searched in hashtable</param>
        /// <returns></returns>
        public bool Contains(K k)
        {
            try {
                if (k == null)
                    throw new ArgumentNullException();
                V value1 = default(V);
                V value2 = Get(k);
                if (!EqualityComparer<V>.Default.Equals(value1, value2))
                    return true;
                else
                    throw new NonExistentKey<K>(k);
            }
            catch (NonExistentKey<K> nek)
            {
                Console.WriteLine(nek);
            }
            catch (ArgumentNullException ane)
            {
                Console.WriteLine(ane.Message);
            }
            return false;
        }

        /// <summary>
        /// Searched the key k in the hashtable and returns its value if the key
        /// exists. Otherwise, returns the default generic value for the value type
        /// V.
        /// </summary>
        /// <param name="k">key whose value needs to be returned</param>
        /// <returns></returns>
        public V Get(K k)
        {
            try
            {
                if (k == null)
                    throw new ArgumentNullException();
                int hashCode = CalculateHashCode(k);
                if (hashTable[hashCode] != null) 
                {
                    Node<K, V> curr = hashTable[hashCode];

                    while (curr.Next != null)
                    {
                        if (EqualityComparer<K>.Default.Equals(curr.Next.Key, k))
                            return curr.Next.Value;
                        curr = curr.Next;
                    }

                    if (curr.Next == null)
                        throw new NonExistentKey<K>(k);
                } else
                {
                    throw new NonExistentKey<K>(k);
                }
            }
            catch (NonExistentKey<K> nek) {
                Console.WriteLine(nek);
            }
            catch (ArgumentNullException ane)
            {
                Console.WriteLine(ane.Message);
            }
            return default(V);
        }


        /// <summary>
        /// Creates an object of Class Node using arguments - key and value. Calls  
        /// CalculateHashCode function to get the hashCode for hashtable. If the bucket
        /// of the hashcode is null then simply inserts the class Node object. 
        /// Otherwise, it checks if key is present otherwise updates value of existing 
        /// key.
        /// </summary>
        /// <param name="key">key of class Node object</param>
        /// <param name="val">value of class Node object</param>
        public void Put(K key, V val)
        {
            try
            {
                if (key == null || val == null)
                {
                    throw new ArgumentNullException();
                }
                Node<K, V> node = new Node<K, V>();
                node.Key = key;
                node.Value = val;
                int hashCode = CalculateHashCode(key);

                if (hashTable[hashCode] == null)
                {
                    Node<K, V> dummy = new Node<K, V>();
                    dummy.Key = default(K);
                    dummy.Value = default(V);
                    dummy.Next = node;

                    hashTable[hashCode] = dummy;
                    CurrentCount++;
                    if (CurrentCount >= (int)hashTable.Length * loadThreshold)
                        ReHash();
                }
                else
                {
                    Node<K, V> curr = hashTable[hashCode];

                    while (curr.Next != null && !EqualityComparer<K>.Default.Equals(curr.Next.Key, node.Key))
                    {
                        curr = curr.Next;
                    }

                    if (curr.Next == null)
                    {
                        curr.Next = node;
                        CurrentCount++;
                        if (CurrentCount >= (int)hashTable.Length * loadThreshold)
                            ReHash();
                    }
                    else if (EqualityComparer<K>.Default.Equals(curr.Next.Key, node.Key))
                    {
                        curr.Value = node.Value;
                    }

                }
            }
            catch (ArgumentNullException ane) {
                Console.WriteLine(ane.Message);
            }
            
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Enumerates through the hashTable and using yield returns the current
        /// key of the element in the hashTable.
        /// </summary>
        /// <returns>IEnumerator object</returns>
        public IEnumerator<K> GetEnumerator()
        {
            for (int i=0; i< hashTable.Length; i++)
            {
                if (hashTable[i] == null)
                    continue;

                Node<K, V> curr = hashTable[i];

                while (curr.Next != null)
                {
                    yield return curr.Next.Key;
                    curr = curr.Next;
                }
            }
        }

        /// <summary>
        /// Rehashes the hashTable when the current element count is greater than or
        /// equal to threshold value. When this condition is met, it updates the size of 
        /// the existing hashTable by copying it into a new HashTable object.
        /// </summary>
        public void ReHash()
        {

            Node<K, V>[] newHashTable = new Node<K, V>[hashTable.Length];

            for (int i=0; i<hashTable.Length; i++)
            {
                if (hashTable[i] == null)
                    continue;
                newHashTable[i] = hashTable[i];
            }

            hashTable = new Node<K, V>[(int) (Math.Ceiling(hashTable.Length * 1.5))];
            CurrentCount = 0;

            for (int i = 0; i < newHashTable.Length; i++)
            {
                if (newHashTable[i] == null)
                    continue;
                Node<K, V> curr = newHashTable[i];

                while (curr.Next != null)
                {
                    Put(curr.Next.Key, curr.Next.Value);
                    curr = curr.Next;
                }
            }
        }
    }

    /// <summary>
    /// Class Node which has key, value and next as data members. The object of this class
    /// is chainable and are used by hashtable objects of LinkedHashTable class.
    /// </summary>
    /// <typeparam name="K">Key for hashtable</typeparam>
    /// <typeparam name="V">Value corresponding to a key</typeparam>
    public class Node<K, V> {
        private K key;
        private V val;

        public Node<K, V> Next { get; set; } = null;

        public K Key { get => key; set => key = value; }
        public V Value { get => val; set => val = value; }
    }

}
