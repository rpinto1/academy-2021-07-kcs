import React, { useEffect, useState } from 'react'
import { useDispatch, useSelector } from 'react-redux'
import { Button, Grid, Menu } from 'semantic-ui-react'
import './countrypicker.css'

export default function CountryPicker() {

    const [countrys, setCountrys] = useState([])
    const activeItems = useSelector(state => state.countries)
    const urlPage = useSelector(state => state.url);
    const dispatch = useDispatch();

    

    useEffect(() => {
        var data = fetch(`${urlPage}api/Companies/countries`)
        .then(response => response.json());
        data.then(data => data["result"].map(x=>({
            key: x["name"],
            text: x["fullName"],
            value: x["name"],
        })) )
        .then(arrayFinal => setCountrys((prevState) => [...prevState, ...arrayFinal]))
    }, [])

   const handleItemClick = (e, { name }) => {
    console.log("hello")
        let nameIndex = activeItems.findIndex(country => country === name)
        console.log(nameIndex)
       if(activeItems.length == 1 && nameIndex >= 0) {return;}     
       if(nameIndex >= 0){
        console.log("delete")
        dispatch({
            type:'DELETE_COUNTRY',
            index: nameIndex
        })
        return;
       }
       dispatch({
        type:'ADD_COUNTRY',
        country: name
        })
    }

    return (
        <Grid padded="vertically">
            <Grid.Row textAlign="center" verticalAlign="middle" centered>
                <Menu compact>
                        {
                            
                        countrys.map((country,i) => 
                        <Menu.Item 
                        key = {i}
                        name={country.key}
                        active={(activeItems.findIndex(ok => ok ==country.key))!==-1}
                        onClick={handleItemClick}
                        >{country.text}</Menu.Item>) 
                        }
                    </Menu>
            </Grid.Row>





        </Grid>

    )
}
