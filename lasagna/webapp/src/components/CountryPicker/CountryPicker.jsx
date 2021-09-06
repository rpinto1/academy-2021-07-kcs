import React, { useEffect, useState } from 'react'
import { useDispatch, useSelector } from 'react-redux'
import { Button, Grid, Menu } from 'semantic-ui-react'
import './countrypicker.css';
import { countryAdd, countryDelete } from '../../redux/countriesReducer';

export default function CountryPicker() {

    const [countrys, setCountrys] = useState([])
    const activeItems = useSelector(state => state.countries)
    const dispatch = useDispatch();

    

    useEffect(() => {
        var data = fetch(`http://localhost:3010/api/Companies/countries`)
        .then(response => response.json());
        data.then(data => data["result"].map(x=>({
            key: x["name"],
            text: x["fullName"],
            value: x["name"],
        })) )
        .then(arrayFinal => setCountrys((prevState) => [...prevState, ...arrayFinal]))
    }, [])

   const handleItemClick = (e, { name }) => {
        let nameIndex = activeItems.findIndex(country => country === name)
       if(activeItems.length == 1 && nameIndex >= 0) {return;}     
       if(nameIndex >= 0){
        dispatch(countryDelete(nameIndex))
        return;
       }
       dispatch(countryAdd(name))
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
