const Calculator = {
    calculateSum: function (numbers) {
        let result = numbers.reduce((sum, a) => sum + a, 0);
        return result;
    },


    findMax: function (arr) {
        return Math.max(...arr);
    },

    //Maybe array.reduce?
    generateFibonacci: function (n) {
        let fibonacci = [0, 1];
        for (let i = 2; i < n; i++) {
            let num = fibonacci[i - 1] + fibonacci[i - 2];
            fibonacci.push(num);
        }
        return fibonacci;
    },

    convertToBinary: function (num) {
        return num.toString(2);
    },

    getRandomNumber: function (min, max) {
        return Math.floor(Math.random() * (max - min + 1)) + min;
    },
    
    filterArray: function (arr, condition) {
        let filteredArray = arr.filter(n=> condition(n))
        return filteredArray;
    }
};

const StringManipulator = {
    // This method capitalizes the string input and returns it as a string output with capital letter.
    capitalizeString: function (str) {
        let words = str.split(" ");
        let result =  words.map(w=> this.capitalizeWord(w));
        return result.join(" ");
    },
    capitalizeWord: function(word){
        return word.charAt(0).toUpperCase() + word.slice(1);
    },
    checkPalindrome: function (str) {
        let reversed = str.split("").reverse().join("");
        return str == reversed;
    },
}

// Example usage
const numbers = [1, 2, 3, 4, 5];
const str = "hello world";
const arr = [3, 1, 7, 5, 2, 9];

console.log("Sum:", Calculator.calculateSum(numbers));
console.log("Capitalized String:", StringManipulator.capitalizeString(str));
console.log("Max Value:", Calculator.findMax(arr));
console.log("Is Palindrome:", StringManipulator.checkPalindrome("civic"));
console.log("Fibonacci Sequence:", Calculator.generateFibonacci(10));
console.log("Binary Representation:", Calculator.convertToBinary(42));
console.log("Random Number:", Calculator.getRandomNumber(1, 100));
console.log("Filtered Array:", Calculator.filterArray(arr, (num) => num % 2 === 0));