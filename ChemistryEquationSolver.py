import numpy as np

def check_balance(chemicals):

    number_of_element = {}

    for chemical in chemicals:
        global_multiplyer = ""
        chemical_name = ""
        chemical_multiplyer = ""

        isCheckingMolecule = False

        for letter in chemical:
            if not isCheckingMolecule:
                try:
                    global_multiplyer += str(int(letter))
                except ValueError:
                    chemical_name += letter
                    isCheckingMolecule = True
                
            elif isCheckingMolecule:
                if not letter.isdigit() and not letter.isupper():
                    chemical_name += letter
                    continue

                elif letter.isdigit():
                    chemical_multiplyer += letter
                    continue

                if chemical_multiplyer == "":
                    chemical_multiplyer = "1"
                count_occurances(number_of_element, chemical_name, global_multiplyer, chemical_multiplyer)

                chemical_name = letter
                chemical_multiplyer = ""

        count_occurances(number_of_element, chemical_name, global_multiplyer, chemical_multiplyer)

    return number_of_element

def count_occurances(number_of_element, reactant_name, global_multiplyer, chemical_multiplyer):
    if global_multiplyer == "":
        global_multiplyer = "1"

    if chemical_multiplyer == "":
        chemical_multiplyer = "1"

    number_of_element[reactant_name] = int(number_of_element.get(reactant_name, 0))+(int(global_multiplyer) * int(chemical_multiplyer))

print("Welcome to Ahkam's Chemistry Equation Solver!")
input_equation=input("What equation would you like to check/balance: ").replace(" ","")

equation_halfs = input_equation.split("=")

reactants = equation_halfs[0].split("+")
products = equation_halfs[1].split("+")

print("\nWould you like to CHECK or BALANCE (Not Implemented)?")

number_in_reactants = check_balance(reactants)
number_in_products = check_balance(products)

list_of_elements = number_in_reactants.copy()
list_of_elements.update(number_in_products)

print()

while True:
    choice = input()
    if choice == "CHECK":
        for key in list_of_elements:
            number_in_reactant = number_in_reactants.get(key, 0)
            number_in_product = number_in_products.get(key, 0)

            print("Element ({0}):\n    Occurs {1} times in the reactants\n    Occurs {2} times in the products".format(key, number_in_reactant, number_in_product))
            if number_in_reactant > number_in_product:
                print("    Number in reactants exceed products by {0}\n\n".format(number_in_reactant-number_in_product))
            elif number_in_reactant < number_in_product:
                print("    Number in products exceed reactants by {0}\n\n".format(number_in_product-number_in_reactant)) 
            else:
                print("    The values are equal \n")
        break

    elif choice == "BALANCE":
        if number_in_reactants.keys() != number_in_products.keys():
            print("Sorry, but certain elements are not present on both the reactants and products, thus impossible to balance")
        else:
            reactant_matrix = [[0] * (len(reactants)+len(products)) for i in range(len(list_of_elements))]
            product_matrix = [[0] * (len(reactants)+len(products)) for i in range(len(list_of_elements))]

            for i, element in enumerate(list_of_elements):
                for j, chemical in enumerate(reactants):
                    elements_in_single_reactant = check_balance([chemical])
                    reactant_matrix[i][j] = elements_in_single_reactant.get(element, 0)


            for i, element in enumerate(list_of_elements):
                for j, chemical in enumerate(products):
                    elements_in_single_reactant = check_balance([chemical])
                    reactant_matrix[i][j+len(reactants)] = -1*elements_in_single_reactant.get(element, 0)

            #reactant_matrix[len(reactant_matrix)-1][0] = 1
            left = np.matrix(reactant_matrix)
            right = np.matrix([0,0])

            sol = np.linalg.lstsq(left, right)

            print(sol)

            '''reactants_numbers = [None]*len(reactants)
            products_numbers = [None]*len(products)

            for i, reactant in enumerate(reactants):
                reactants_numbers[i] = check_balance(reactant)

            for i, product in enumerate(products):
                reactants_numbers[i] = check_balance(product)
            
            total_element_in_reactants, total_element_in_products = [None]*len(list_of_elements), [None]*len(list_of_elements)

            for i, key in enumerate(list_of_elements):
                for j, reactant in enumerate(reactants):
                    total_element_in_reactants[i] += reactant[key]
                
                for j, product in enumerate(products):
                    total_element_in_products[i] += product[key]'''

    else:
        print("Sorry, but what you entered was not a valid option. Try another option")




                
