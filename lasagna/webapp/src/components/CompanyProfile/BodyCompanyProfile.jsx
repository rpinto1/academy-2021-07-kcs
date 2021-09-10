import React, { useState } from 'react'
import { Radio, Container, Form } from 'semantic-ui-react';
import CompanyTitleAndLink from './CompanyTitleAndLink';
import RuleOnegraph from './RuleOnegraph';
import $ from 'jquery';



export default function BodyCompanyProfile() {
    
    //Return on investment
    // Sales growth rate
    //Earnings per share
    //Book value of equity per share 
    //Free cash flow
    
    const [selected, setSelected] = useState({
        option: 'ROIC'
    })
          
    const handleChange = (event, {value}) => {
        setSelected({
            option : value
        });
    };

  

    return (
        <>
        <Container> 
            <CompanyTitleAndLink />           
        </Container>
                <Container className="ui fluid two column divided grid ">

                    <Form className = 'ui list' id="checkboxes-list" >

                            <Form.Field>
                                <Radio 
                                name='aspect'
                                label='ROIC'
                                value= 'ROIC' 
                                checked = {selected.option == 'ROIC'}
                                onChange={handleChange}/>
                            </Form.Field>
                            <Form.Field>
                                <Radio name='aspect'
                                label='Sales growth rate' 
                                value='SGR'
                                checked = {selected.option == 'SGR'}
                                onChange={handleChange}/>
                            </Form.Field>
                            <Form.Field>
                                <Radio name='aspect'
                                label='Earnings per share'
                                value='EPS'
                                checked = {selected.option == 'EPS'}
                                onChange={handleChange}/>
                            </Form.Field>
                            <Form.Field>
                                <Radio name='aspect'
                                label='Equity growth rate' 
                                value= 'BVPS'
                                checked = {selected.option == 'BVPS'}
                                onChange={handleChange}/>
                            </Form.Field>
                            <Form.Field>
                                <Radio name='aspect'
                                label='Cash Flow' 
                                value='FCF'
                                checked = {selected.option == 'FCF'}
                                onChange={handleChange}/>                            
                            </Form.Field>

                        <RuleOnegraph selected={selected}/>
                    </Form>

                </Container>
    
        </>
    )
}