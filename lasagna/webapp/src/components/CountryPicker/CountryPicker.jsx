import React, { useEffect, useState } from 'react'
import { Menu } from 'semantic-ui-react'
import { Store } from 'redux'

export default function CountryPicker() {

    const [countrys, setCountrys] = useState(["US"])
    const activeItems = useSelector(state => state.countries)


    useEffect(() => {
        var data = fetch(process.env.REACT_APP_URL+'/api/Companies/countries')
        .then(response => response.json());
        data.then(data => data["result"].map(x=>({
            key: x["name"],
            text: x["fullName"],
            value: x["name"],
        })) )
        .then(arrayFinal => setindex((prevState) => [...prevState, ...arrayFinal]))
    }, [])

   const handleItemClick = (e, { name }) => {
       if(activeItems.length <=1) {return;}
       let nameIndex = activeItems.findIndex(country => country === name)
       if(nameIndex in activeItems){

        const dispatch = useDispatch();
        setActiveItems(prevState => {
            prevState.filter(item => item != name)
        })
       }
       setActiveItems(prevState => [...prevState,name])
    }

    return (
        <Menu compact>
            {
                
               countrys.map((country,i) => 
               <Menu.Item
               name={country}
               active={activeItems.findIndex(ok => ok ===country)!==-1}
               content={country}
               onClick={handleItemClick}
             />) 
            }
            

        </Menu>
    )
}
