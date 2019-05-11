import React, { Component } from 'react';
import {Table, Button} from 'element-react/next';
import cssClasses from './productListing.module.css';
import {withRouter} from 'react-router-dom';

class ProductListing extends Component {
  constructor(props) {
    super(props);
    this.state = {
      columns: [
        {
          label: "Image",
          prop: "image",
          width: 250,
          align: "center",
          render: function(data){
            return (
            <span>
              <img src={data.Image} className={cssClasses.tableImage} alt="Product's"/>
            </span>)
          }
        },
        {
          label: "Name",
          prop: "name",
          width: 200,
          align: "center",
          render: function(data){
            return (<>{data.Name}</>);
          }
        },
      
        {
          label: "Price",
          prop: "price",
          width: 200,
          align: "center",
          render: function(data){
            return (<>{data.Price}</>);
          }
        },
        {
          label: "Producer",
          prop: "company",
          width: 200,
          align: "center",
          render: function(data){
            return (<>{data.Company.Name}</>);
          }
        },
        {
          label: "",
          width: 300,
          align: "center",
          render: (data) => {
            return (
              <span>
               <Button type="info" size="small" onClick={()=>{this.props.history.push(`products/edit/${data.Id}`)}}>Edit</Button>
              </span>
            )
          }
        }
      ],
      data: this.props.products
    }
  }
  
  tableStyle = {
    marginLeft: 'auto',
    fontWeight: 'bold'
  }
  render() {
    return (
      <Table
        style={this.tableStyle}
        columns={this.state.columns}
        data={this.state.data}
        border={true}
        highlightCurrentRow={true}
      />
    )
  }
}

export default withRouter(ProductListing);
