import React, { useEffect, useState } from 'react'
import { Radio, Container, Form, Table } from 'semantic-ui-react';
import CompanyTitleAndLink from './CompanyTitleAndLink';
import RuleOnegraph from './RuleOnegraph';



export default function BodyCompanyProfile({companyInfo}) {

    const { ticker, name } = companyInfo;

    const [selected, setSelected] = useState({
        ticker,
        option: 'ROIC',
        label: 'Return on Invested Capital'
    })
    
    useEffect(() => {
        setSelected({
            ticker,
            option: 'ROIC',
            label: 'Return on Invested Capital'
        })
    }, [ticker])

    const handleChange = (event, {value, label}) => {
        setSelected({
            label: label, 
            option : value
        });
    };

    

    const options = [{label:'Return on Invested Capital', value: 'ROIC'},
                     {label:'Dividends per Share', value: 'DPS'},
                     {label:'Earnings per Share', value: 'EPS'},
                     {label:'Earnings per Share Growth', value: 'EPSG'},                     
                     {label:'Gross Margin', value: 'GM'},
                     {label:'Gross Profit', value: 'GS'},
                     {label:'Operating Margin', value: 'OM'},
                     {label:'Operating Profit', value: 'OP'},
                     {label:'Return on Assets', value: 'ROA'},
                     {label:'Return on Equity', value: 'ROE'},
                     {label:'Revenue', value: 'R'},
                     {label:'Revenue Growth', value: 'RG'}]

    return (
        <>
        <Container> 
            <CompanyTitleAndLink companyInfo={companyInfo}/>
        </Container>
        <Container>
        <Table definition>
            <Table.Body>
                <Table.Row>
                    <Table.Cell width={6}>
                    
                        <Form className = 'ui list' id="checkboxes-list" >

                        {options.map((o,i) => 
                            <Form.Field>
                                <Radio 
                                name='aspect'
                                label= {o.label}
                                value= {o.value}
                                checked = {selected.option === o.value}
                                id={i}
                                onChange={handleChange}/>
                        </Form.Field>                       
                        )}
                
                        </Form>
                    </Table.Cell>
                    <Table.Cell>
                        <RuleOnegraph selected={selected}/>
                     </Table.Cell>
                </Table.Row>
            </Table.Body>
        </Table>
                    
        </Container>            
                    
            
    
        </>
    )
}