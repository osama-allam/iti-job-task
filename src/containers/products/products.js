import React, { Component } from 'react';
import ProductListing from '../productListing';
import SideMenu from '../../components/sideMenu';
import {Layout, Loading, Pagination, Input, Button} from 'element-react/next';
import * as ProductsDB from '../../api/productsApi';
import * as CompaniesDB from '../../api/companiesApi';
import * as cssClasses from './products.module.css';

class Products extends Component {
  constructor(props)
  {
    super(props);
    this.state = {
      companies: null,
      products: null,
      loading: true,
      total: 0,
      pageSize: 3,
      currentPage: 1,
      companyName: "All",
      searchQuery: ""
    }
    this.getProducts = this.getProducts.bind(this);
    this.pageChanged = this.pageChanged.bind(this);
    this.filterChanged = this.filterChanged.bind(this);
  }

  getProducts = (currentPage, companyName, searchQuery) => {
    this.setState({loading: true});
    ProductsDB.getAll(this.state.pageSize, currentPage, companyName, searchQuery)
    .then( res => {
      const total = JSON.parse(res.headers["x-pagination"]).totalCount;
      this.setState({
        products: res.data,
        total
      });
      setTimeout(()=>{this.setState({loading:false})}, 1000);
    }
    );
  }
  componentDidMount()
  {
    this.getProducts(this.state.currentPage);
    CompaniesDB.getAll()
    .then( res => this.setState({companies: res.data})
    );
  }
  pageChanged = (currentPage) => {
    this.setState({currentPage});
    this.getProducts(currentPage, this.state.companyName, this.state.searchQuery);
  }
  filterChanged = (index) => {
    this.getProducts(1,index, this.state.searchQuery);
    this.setState({
      currentPage: 1,
      companyName: index
    });
  }
  searchChanged = (value) => {
    this.setState({searchQuery: value});
  }
  search = () => {
    this.setState({currentPage: 1});
    this.getProducts(1,this.state.companyName,this.state.searchQuery);
  }
  reset = () => {
    this.setState({
      currentPage: 1,
      companyName: "All",
      searchQuery: ""
    });
    this.getProducts(1);
  }
  render() {
    let productsTable = null;
    if(this.state.products)
    {
      productsTable = (this.state.products.length > 0) ? (<ProductListing products={this.state.products}/>) : (<h3>No Products Found</h3>);
    } 
    return (
    <Loading loading={this.state.loading} className={cssClasses.layout}>
      <Layout.Row gutter="0">
        <Layout.Col span="4">
          <Button type="warning" size="mini" onClick={this.reset}>Reset All</Button>
          <Input placeholder="Search ..." value={this.state.searchQuery} onChange={this.searchChanged}
            append={<Button onClick={this.search} type="success" icon="search"></Button>} />
          <SideMenu onSelect={this.filterChanged} filter={this.state.companyName} companies={this.state.companies}/>
        </Layout.Col>
        <Layout.Col span="19" offset="1">
          {productsTable}
          <Pagination onCurrentChange={this.pageChanged} className={cssClasses.pagination}  layout="prev, pager, next" pageSize={this.state.pageSize} total={this.state.total} currentPage={this.state.currentPage}/>
        </Layout.Col>
      </Layout.Row>
    </Loading>
    );
  }
}

export default Products;
