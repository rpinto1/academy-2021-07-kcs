import _ from 'lodash'
import React, { useState } from 'react'
import { Dropdown, List, Segment } from 'semantic-ui-react'
import { Company } from './Company'

export const IISList = () => {

    const [options, setOptions] = useState(["hello","Goodbye"])
    const getOptions = (number, prefix = 'Choice ') =>
            _.times(number, (index) => ({
                key: index,
                text: `${prefix}${index}`,
                value: index,
  }))

    var arrayCompanys = []
    for (let index = 0; index < 100; index++) {
        arrayCompanys.push({});
        
    }

    return (
        <Segment textAlign='left' className='segment'>
            <h1>List of Companies</h1>
            <Segment.Inline >
            <Dropdown placeholder='Index' search selection options={options} scrolling options={getOptions(15)}/>
            <Dropdown placeholder='Sector' search selection options={options} scrolling options={getOptions(15)}/>
            <Dropdown placeholder='Industry' search selection options={options} scrolling options={getOptions(15)}/>
            </Segment.Inline>
            
            <List celled relaxed size='tiny' className="list">
                {
                    arrayCompanys.map(x=> <Company />)
                }
                <Company />
                <Company />
            </List>
        </Segment>
            
        
    )
}
